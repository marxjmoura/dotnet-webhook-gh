# dotnet-webhook-gh

.NET Core webhook to listen for GitHub events.

## How to

How to run the API locally:

`dotnet run --project src/DotnetWebhookGH.Api`

How to run the automated tests:

`dotnet test src`

## DynamoDB - Data modeling

Table: dotnet-webook-gh

<table>
  <tr>
    <th>Partition Key</th>
    <th>Sort Key</th>
    <th>Attributes</th>
  <tr>
  <tr>
    <td>{repo_full_name}/issues</td>
    <td>#{issue_number} {updated_at}</td>
    <td>
     {
        PK,
        SK,
        <a href="https://docs.github.com/developers/webhooks-and-events/webhooks/webhook-events-and-payloads#issues">
          event
        </a>
      }
    </td>
  </tr>
</table>
