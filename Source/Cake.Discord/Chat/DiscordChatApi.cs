using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Discord.LitJson;

namespace Cake.Discord.Chat
{
  internal static class DiscordChatApi
  {
    internal static async Task<DiscordChatMessageResult> PostMessage(
        this ICakeContext context,
        string webHookUrl,
        string content,
        DiscordChatMessageSettings messageSettings)
    {
      if (messageSettings == null)
      {
        throw new ArgumentNullException(nameof(messageSettings), "Invalid Discord message specified");
      }

      if (string.IsNullOrWhiteSpace(webHookUrl))
      {
        throw new NullReferenceException("Invalid WebHookUrl supplied.");
      }

      context.Verbose(
          "Posting to incoming webhook {0}...",
          string.Concat(
              webHookUrl
                  .TrimEnd('/')
                  .Reverse()
                  .SkipWhile(c => c != '/')
                  .Reverse()
          )
      );

      var json = ToJson(
          new
          {
            content,
            username = messageSettings.UserName ?? "CakeBuild",
            avatar_url = messageSettings.AvatarUrl?.ToString() ?? new Uri("https://raw.githubusercontent.com/cake-build/graphics/master/png/cake-small.png").ToString(),
            tts = messageSettings.Tts
          });

      context.Debug("Parameter: {0}", json);

      using (var client = new HttpClient())
      {
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

        var httpResponse = await client.PostAsync(webHookUrl, stringContent);
        context.Debug($"Status Code: {httpResponse.StatusCode}");

        DiscordChatMessageResult parsedResult = null;
        if(httpResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
        {
          var response = await httpResponse.Content.ReadAsStringAsync();
          context.Debug($"Response: {response}");

          var result = JsonMapper.ToObject(response);

          parsedResult = new DiscordChatMessageResult(
            false,
            DateTime.UtcNow.ToString(),
            result.GetInteger("code").Value,
            result.GetString("message"));
        }
        else
        {
          parsedResult = new DiscordChatMessageResult(
            true,
            DateTime.UtcNow.ToString(),
            0,
            string.Empty);
        }

        context.Debug("Result parsed: {0}", parsedResult);

        if (!parsedResult.Ok && messageSettings.ThrowOnFail == true)
        {
          throw new CakeException(parsedResult.Error ?? "Failed to send message, unknown error");
        }

        return parsedResult;
      }
    }

    private static int? GetInteger(this JsonData data, string key)
    {
      return (data != null && data.Keys.Contains(key))
          ? (int)data[key]
          : null as int?;
    }

    private static string GetString(this JsonData data, string key)
    {
      return (data != null && data.Keys.Contains(key))
          ? (string)data[key]
          : null;
    }

    private static string ToJson(object obj)
    {
      var jsonWriter = new JsonWriter
      {
        LowerCaseProperties = true
      };
      JsonMapper.ToJson(obj, jsonWriter);
      return jsonWriter.ToString();
    }
  }
}
