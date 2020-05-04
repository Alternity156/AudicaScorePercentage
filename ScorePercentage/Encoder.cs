using SimpleJSON;

namespace ScorePercentage
{
    class Encoder
    {
        public static string GetConfig(Config config)
        {
            JSONObject configJSON = new JSONObject();

            configJSON["leaderboardUserColor"] = config.leaderboardUserColor;
            configJSON["leaderboardHighScoreSize"] = config.leaderboardHighScoreSize;
            configJSON["leaderboardPercentSize"] = config.leaderboardPercentSize;
            configJSON["leaderboardUsernameSize"] = config.leaderboardUsernameSize;
            configJSON["songListHighScoreSize"] = config.songListHighScoreSize;
            configJSON["songListPercentSize"] = config.songListPercentSize;
            configJSON["inGameCurrentScoreSize"] = config.inGameCurrentScoreSize;
            configJSON["inGameCurrentPercentSize"] = config.inGameCurrentPercentSize;
            configJSON["inGameHighScoreLabelText"] = config.inGameHighScoreLabelText;
            configJSON["inGameHighScoreLabelSize"] = config.inGameHighScoreLabelSize;
            configJSON["inGameHighScoreSize"] = config.inGameHighScoreSize;
            configJSON["inGamePercentSize"] = config.inGamePercentSize;
            configJSON["historyTopScoreSize"] = config.historyTopScoreSize;
            configJSON["historyTopPercentSize"] = config.historyTopPercentSize;

            return configJSON.ToString(4);
        }

        public static void SetConfig(Config config, string data)
        {
            var configJSON = JSON.Parse(data);

            config.leaderboardUserColor = configJSON["leaderboardUserColor"];
            config.leaderboardHighScoreSize = configJSON["leaderboardHighScoreSize"];
            config.leaderboardPercentSize = configJSON["leaderboardPercentSize"];
            config.leaderboardUsernameSize = configJSON["leaderboardUsernameSize"];
            config.songListHighScoreSize = configJSON["songListHighScoreSize"];
            config.songListPercentSize = configJSON["songListPercentSize"];
            config.inGameCurrentScoreSize = configJSON["inGameCurrentScoreSize"];
            config.inGameCurrentPercentSize = configJSON["inGameCurrentPercentSize"];
            config.inGameHighScoreLabelText = configJSON["inGameHighScoreLabelText"];
            config.inGameHighScoreLabelSize = configJSON["inGameHighScoreLabelSize"];
            config.inGameHighScoreSize = configJSON["inGameHighScoreSize"];
            config.inGamePercentSize = configJSON["inGamePercentSize"];
            config.historyTopScoreSize = configJSON["historyTopScoreSize"];
            config.historyTopPercentSize = configJSON["historyTopPercentSize"];
        }
    }
}
