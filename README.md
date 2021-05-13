## Dockord.NET Discord Bot
A boilerplate DSharpPlus Discord bot on .NET 5 using Docker

### Basic Setup
1. You will need to have a version [Docker Compose](https://docs.docker.com/compose/install/) installed that is 
compatible with version => 3.4 docker-compose files.
2. Next, you will need to setup a Discord application and bot on the [Discord Developer Portal](https://discord.com/developers/applications) 
if you do not already have one. Take note of the bot's token.
3. You can modify the default bot configuration settings by editing the environment section of the 
`docker-compose.override.yml` file or the `appsettings.Development.json` file. The docker-compose environment 
variables have the highest priority over other configuration files. 

* **IMPORTANT**: Make sure to add the Discord bot's token to either the `BotSettings__Token` environment variable or
the `Token` key in the `appsettings.Development.json` file otherwise the bot will fail to start.

4. Once all the services are running, you can access the bot's logs on the [local Seq instance](http://localhost:5340/).

### Example Commands
##### confirmordeny
The `confirmordeny` command allows you to gamify a question (Optionally to a difference channel). Multiword sentences 
should be wrapped in quotes (ie; "The quick brown fox"). The delay parameter allows you to add a delayed timelimit 
for the question to be answered.
```
!confirmordeny <question> [channelName] [delay]
```


##### deletemessages
The `deletemessages` command deletes the number of messages specified from the channel it is sent to. This command 
requires owner permissions.
```
!deletemessages [limit]
```