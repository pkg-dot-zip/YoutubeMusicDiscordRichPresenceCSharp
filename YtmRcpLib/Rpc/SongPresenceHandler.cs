﻿using DiscordRPC;
using YtmRcpLib.Models;

namespace YtmRcpLib.Rpc;

public static class SongPresenceHandler
{
    public static RichPresence GetSongPresence(CurrentPlayingInfo info)
    {
        return new RichPresence()
        {
            Type = ActivityType.Listening,
            Details = GetPresenceDetails(info),
            Timestamps = GetPresenceTimestamps(info),
            Assets = GetPresenceAssets(info),
            Buttons = GetPresenceButtons(info).ToArray()
        };
    }

    private static List<Button> GetPresenceButtons(CurrentPlayingInfo info)
    {
        var buttons = new List<Button>(3);

        if (info.SongUrl != string.Empty)
        {
            Console.WriteLine("Adding listen on YT button.");
            buttons.Add(new Button()
            {
                Label = "Listen on Youtube Music",
                Url = $"{info.SongUrl}",
            });
        }

        Console.WriteLine("Adding install client button.");

        buttons.Add(new Button()
        {
            Label = "Install YTM RPC Client",
            Url = "https://github.com/pkg-dot-zip/YoutubeMusicDiscordRichPresenceCSharp",
        });

        if (buttons.Count > 2) throw new InvalidOperationException("RPC has a limit of 2 buttons!");
        return buttons;
    }

    private static Assets GetPresenceAssets(CurrentPlayingInfo info)
    {
        return new Assets()
        {
            LargeImageKey = $"{info.ArtworkUrl}",
            LargeImageText = $"{info.Album}",
            SmallImageKey =
                "https://uxwing.com/wp-content/themes/uxwing/download/brands-and-social-media/youtube-music-icon.png",
        };
    }

    private static Timestamps GetPresenceTimestamps(CurrentPlayingInfo info)
    {
        return new Timestamps()
        {
            Start = DateTime.UtcNow.AddSeconds(-info.CurrentTime),
            End = DateTime.UtcNow.AddSeconds(info.RemainingTime)
        };
    }

    private static string GetPresenceDetails(CurrentPlayingInfo info) => $"{info.Artist} - {info.Title}";
}