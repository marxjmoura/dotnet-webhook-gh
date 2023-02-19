namespace DotnetWebhookGH.Api.Features;

using DotnetWebhookGH.Api.Data;
using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Data.Model.Labels;
using DotnetWebhookGH.Api.Data.Model.Repositories;
using DotnetWebhookGH.Api.Data.Model.Users;
using DotnetWebhookGH.Api.Payloads.Events;
using DotnetWebhookGH.Api.Payloads.Issues;
using DotnetWebhookGH.Api.Payloads.Repositories;
using DotnetWebhookGH.Api.Payloads.Users;
using Microsoft.EntityFrameworkCore;

public class IssueSnapshot
{
    private readonly ApiDbContext _dbContext;

    public IssueSnapshot(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(string delivery, EventJson payload)
    {
        var received = await _dbContext.Issues
            .WhereDelivery(delivery)
            .AnyAsync();

        if (received) return;

        var knownUsers = await _dbContext.Users
            .WhereIdIn(payload.ReferencedUserIds())
            .ToListAsync();

        var issue = new Issue { Delivery = delivery };
        issue.Repository = await FindRepository(payload);
        issue.Labels = await FindLabels(payload);

        payload.Map(issue, knownUsers);

        _dbContext.Add(issue);

        await _dbContext.SaveChangesAsync();
    }

    private async Task<ICollection<Label>> FindLabels(EventJson payload)
    {
        return await _dbContext.Labels
            .WhereIdIn(payload.Issue!.AllLabelIds())
            .ToListAsync();
    }

    private async Task<Repository?> FindRepository(EventJson payload)
    {
        return await _dbContext.Repositories
            .WhereId(payload.Repository!.Id!.Value)
            .SingleOrDefaultAsync();
    }
}
