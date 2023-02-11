# dotnet-webhook-gh

.NET Core webhook to listen for GitHub events.

## DynamoDB - Data modeling


Table: dotnet-webook-gh

<table>
  <tr>
    <th>Partition Key</th>
    <th>Sort Key</th>
    <th>Attributes</th>
  <tr>
  <tr>
    <td>Account#{Username}</td>
    <td>User#{Username}</td>
    <td>
      <a href="src/DotnetWebhookGH.Data/DynamoDB/DataModel/Users/User.cs">
        User.cs
      </a>
    </td>
  </tr>
  <tr>
    <td>Account#{Username}#Repositories</td>
    <td>Repository#{Owner}#{Name}</td>
    <td>
      <a href="src/DotnetWebhookGH.Data/DynamoDB/DataModel/Repositories/Repository.cs">
        Repository.cs
      </a>
    </td>
  </tr>
  <tr>
    <td>Account#{Username}#Repository#{Owner}#{Name}#{Event}</td>
    <td>Event#{Name}#{Ulid}</td>
    <td>
      <a href="src/DotnetWebhookGH.Data/DynamoDB/DataModel/Events/Event.cs">
        Event.cs
      </a>
    </td>
  </tr>
</table>
