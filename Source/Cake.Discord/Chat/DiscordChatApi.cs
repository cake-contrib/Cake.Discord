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
          });

      context.Debug("Parameter: {0}", json);

      using (var client = new HttpClient())
      {
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

        var httpResponse = await client.PostAsync(webHookUrl, stringContent);

        var response = await httpResponse.Content.ReadAsStringAsync();
        context.Debug($"Status Code: {httpResponse.StatusCode}");
        context.Debug($"Response: {response}");

        var parsedResult = new DiscordChatMessageResult(
            StringComparer.OrdinalIgnoreCase.Equals(response, "ok"),
            string.Empty,
            StringComparer.OrdinalIgnoreCase.Equals(response, "ok") ? string.Empty : response
        );

        context.Debug("Result parsed: {0}", parsedResult);

        if (!parsedResult.Ok && messageSettings.ThrowOnFail == true)
        {
          throw new CakeException(parsedResult.Error ?? "Failed to send message, unknown error");
        }

        return parsedResult;
      }
    }

    private static bool? GetBoolean(this JsonData data, string key)
    {
      return (data != null && data.Keys.Contains(key))
          ? (bool)data[key]
          : null as bool?;
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
