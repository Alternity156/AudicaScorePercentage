using System;
using System.Collections;
using Harmony;
using MelonLoader;
using UnityEngine;

namespace AudicaModding
{
    public class AudicaMod : MelonMod
    {
        public static class BuildInfo
        {
            public const string Name = "ScorePercentage";  // Name of the Mod.  (MUST BE SET)
            public const string Author = "Alternity"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "1.1.1"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
        }

        public static MenuState.State menuState;
        public static MenuState.State oldMenuState;

        public static StarThresholds starThresholds;
        public static string selectedSong;
        public static int ostMaxTotalScore = 0;
        public static int extrasMaxTotalScore = 0;

        public static string leaderboardUserColor = "yellow";
        public static string leaderboardHighScoreSize = "225";
        public static string leaderboardPercentSize = "150";
        public static string leaderboardUsernameSize = "225";
        public static string songListHighScoreSize = "26";
        public static string songListPercentSize = "17";
        public static string inGameCurrentScoreSize = "120";
        public static string inGameCurrentPercentSize = "75";
        public static string inGameHighScoreLabelText = "HIGH SCORE: ";
        public static string inGameHighScoreLabelSize = "55";
        public static string inGameHighScoreSize = "55";
        public static string inGamePercentSize = "40";
        public static string historyTopScoreSize = "20";
        public static string historyTopPercentSize = "14";

        public override void OnApplicationStart()
        {
            HarmonyInstance instance = HarmonyInstance.Create("AudicaMod");
        }

        private void SaveConfig()
        {
            MelonPrefs.SetString("ScorePercentage", "leaderboardUserColor", leaderboardUserColor);
            MelonPrefs.SetString("ScorePercentage", "leaderboardHighScoreSize", leaderboardHighScoreSize);
            MelonPrefs.SetString("ScorePercentage", "leaderboardPercentSize", leaderboardPercentSize);
            MelonPrefs.SetString("ScorePercentage", "leaderboardUsernameSize", leaderboardUsernameSize);
            MelonPrefs.SetString("ScorePercentage", "songListHighScoreSize", songListHighScoreSize);
            MelonPrefs.SetString("ScorePercentage", "songListPercentSize", songListPercentSize);
            MelonPrefs.SetString("ScorePercentage", "inGameCurrentScoreSize", inGameCurrentScoreSize);
            MelonPrefs.SetString("ScorePercentage", "inGameCurrentPercentSize", inGameCurrentPercentSize);
            MelonPrefs.SetString("ScorePercentage", "inGameHighScoreLabelText", inGameHighScoreLabelText);
            MelonPrefs.SetString("ScorePercentage", "inGameHighScoreLabelSize", inGameHighScoreLabelSize);
            MelonPrefs.SetString("ScorePercentage", "inGameHighScoreSize", inGameHighScoreSize);
            MelonPrefs.SetString("ScorePercentage", "inGamePercentSize", inGamePercentSize);
            MelonPrefs.SetString("ScorePercentage", "historyTopScoreSize", historyTopScoreSize);
            MelonPrefs.SetString("ScorePercentage", "historyTopPercentSize", historyTopPercentSize);
        }

        private void LoadConfig()
        {
            leaderboardUserColor = MelonPrefs.GetString("ScorePercentage", "leaderboardUserColor");
            leaderboardHighScoreSize = MelonPrefs.GetString("ScorePercentage", "leaderboardHighScoreSize");
            leaderboardPercentSize = MelonPrefs.GetString("ScorePercentage", "leaderboardPercentSize");
            leaderboardUsernameSize = MelonPrefs.GetString("ScorePercentage", "leaderboardUsernameSize");
            songListHighScoreSize = MelonPrefs.GetString("ScorePercentage", "songListHighScoreSize");
            songListPercentSize = MelonPrefs.GetString("ScorePercentage", "songListPercentSize");
            inGameCurrentScoreSize = MelonPrefs.GetString("ScorePercentage", "inGameCurrentScoreSize");
            inGameCurrentPercentSize = MelonPrefs.GetString("ScorePercentage", "inGameCurrentPercentSize");
            inGameHighScoreLabelText = MelonPrefs.GetString("ScorePercentage", "inGameHighScoreLabelText");
            inGameHighScoreLabelSize = MelonPrefs.GetString("ScorePercentage", "inGameHighScoreLabelSize");
            inGameHighScoreSize = MelonPrefs.GetString("ScorePercentage", "inGameHighScoreSize");
            inGamePercentSize = MelonPrefs.GetString("ScorePercentage", "inGamePercentSize");
            historyTopScoreSize = MelonPrefs.GetString("ScorePercentage", "historyTopScoreSize");
            historyTopPercentSize = MelonPrefs.GetString("ScorePercentage", "historyTopPercentSize");
        }

        private void CreateConfig()
        {
            MelonPrefs.RegisterString("ScorePercentage", "leaderboardUserColor", "yellow");
            MelonPrefs.RegisterString("ScorePercentage", "leaderboardHighScoreSize", "225");
            MelonPrefs.RegisterString("ScorePercentage", "leaderboardPercentSize", "150");
            MelonPrefs.RegisterString("ScorePercentage", "leaderboardUsernameSize", "225");
            MelonPrefs.RegisterString("ScorePercentage", "songListHighScoreSize", "26");
            MelonPrefs.RegisterString("ScorePercentage", "songListPercentSize", "17");
            MelonPrefs.RegisterString("ScorePercentage", "inGameCurrentScoreSize", "120");
            MelonPrefs.RegisterString("ScorePercentage", "inGameCurrentPercentSize", "75");
            MelonPrefs.RegisterString("ScorePercentage", "inGameHighScoreLabelText", "HIGH SCORE: ");
            MelonPrefs.RegisterString("ScorePercentage", "inGameHighScoreLabelSize", "55");
            MelonPrefs.RegisterString("ScorePercentage", "inGameHighScoreSize", "55");
            MelonPrefs.RegisterString("ScorePercentage", "inGamePercentSize", "40");
            MelonPrefs.RegisterString("ScorePercentage", "historyTopScoreSize", "20");
            MelonPrefs.RegisterString("ScorePercentage", "historyTopPercentSize", "14");
        }

        public override void OnLevelWasLoaded(int level)
        {
            if (!MelonPrefs.HasKey("ScorePercentage", "leaderboardUserColor"))
            {
                CreateConfig();
            }
            else
            {
                LoadConfig();
            }
        }

        public static float GetHighScorePercentage(string songID)
        {
            //Get the needed data
            HighScoreRecords.HighScoreInfo highScoreInfo = HighScoreRecords.GetHighScore(songID);
            float highScore = Convert.ToSingle(highScoreInfo.score);
            float maxPossibleScore = Convert.ToSingle(starThresholds.GetMaxRawScore(songID, highScoreInfo.difficulty));

            //Calculate score percentage
            float percentage = (highScore / maxPossibleScore) * 100;

            return percentage;
        }

        //This returns the percentage of the specified score for the specified song and difficulty compared to the max possible score
        public static float GetScorePercentage(string songID, float score, KataConfig.Difficulty difficulty)
        {
            //Get data
            float maxPossibleScore = Convert.ToSingle(starThresholds.GetMaxRawScore(songID, difficulty));

            //Calculate score percentage
            float percentage = (score / maxPossibleScore) * 100;

            return percentage;
        }

        //This is a function for the coroutine
        public static IEnumerator UpdateLeaderboardRowCoroutine(LeaderboardRow leaderboardRow)
        {
            UpdateLeaderboardRow(leaderboardRow);
            return null;
        }

        //The process to update a LeaderboardRow
        public static void UpdateLeaderboardRow(LeaderboardRow leaderboardRow)
        {
            //DoWork function is really intensive so it's called only when absolutely required
            //This function will calculate the total max possible score for either OST or with extras
            void DoWork(bool e)
            {
                Il2CppSystem.Collections.Generic.List<SongList.SongData> songs = SongList.I.songs;

                int totalMaxScore = 0;

                for (int i = 0; i < songs.Count; i++)
                {

                    SongList.SongData song = songs[i];
                    if (e) { if (song.dlc | song.unlockable) { totalMaxScore += starThresholds.GetMaxRawScore(song.songID, KataConfig.Difficulty.Expert); } }
                    if (song.dlc == false && song.unlockable == false && song.extrasSong == false) { totalMaxScore += starThresholds.GetMaxRawScore(song.songID, KataConfig.Difficulty.Expert); }
                }
                if (e) { extrasMaxTotalScore = totalMaxScore; } else { ostMaxTotalScore = totalMaxScore; }
            }

            float score = Convert.ToSingle(leaderboardRow.mData.score.Split('.')[0]);
            float percentage;

            if (menuState != MenuState.State.MainPage) { percentage = GetScorePercentage(selectedSong, score, KataConfig.Difficulty.Expert); }
            else
            {
                LeaderboardDisplay leaderboardDisplay = UnityEngine.Object.FindObjectOfType<LeaderboardDisplay>();
                bool extras = leaderboardDisplay.extrasButton.IsChecked.Invoke();
                DoWork(extras);

                if (extras) { if (extrasMaxTotalScore == 0) { DoWork(extras); } percentage = (score / extrasMaxTotalScore) * 100; }
                else { if (ostMaxTotalScore == 0) { DoWork(extras); } percentage = (score / ostMaxTotalScore) * 100; }
            }

            //Make pretty-ish strings
            leaderboardRow.username.text = "<size=" + leaderboardUsernameSize + ">" + leaderboardRow.username.text + "</size>";
            string scoreString = "<size=" + leaderboardHighScoreSize + ">" + String.Format("{0:n0}", score).Replace(",", " ") + "</size>";
            string percentageString = "<size=" + leaderboardPercentSize + "> (" + String.Format("{0:0.00}", percentage) + "%)</size>";

            //Update label
            if (leaderboardRow.score.text.Contains("<color=yellow>"))
            {
                leaderboardRow.score.text = "<color=" + leaderboardUserColor + ">" + scoreString + percentageString + "</color>";
            }
            else
            {
                leaderboardRow.score.text = scoreString + percentageString;
            }

            if (leaderboardRow.rank.text.Contains("<color=yellow>"))
            {
                leaderboardRow.rank.text = leaderboardRow.rank.text.Replace("<color=yellow>", "<color=" + leaderboardUserColor + ">");
            }

            if (leaderboardRow.username.text.Contains("<color=yellow>"))
            {
                leaderboardRow.username.text = leaderboardRow.username.text.Replace("<color=yellow>", "<color=" + leaderboardUserColor + ">");
            }

        }

        public static void ScoreKeeperDisplayUpdate(ScoreKeeperDisplay scoreKeeperDisplay)
        {
            if (!KataConfig.I.practiceMode)
            {
                int score = ScoreKeeper.I.mScore;
                float percentage = GetScorePercentage(selectedSong, score, KataConfig.I.GetDifficulty());

                //Make pretty-ish strings
                string scoreString = "<size=" + inGameCurrentScoreSize + ">" + String.Format("{0:n0}", score).Replace(",", " ") + "</size>";
                string percentageString = "<size=" + inGameCurrentPercentSize + "> (" + String.Format("{0:0.00}", percentage) + "%)</size>";

                scoreKeeperDisplay.scoreDisplay.text = scoreString + percentageString;

                HighScoreRecords.HighScoreInfo highScoreInfo = HighScoreRecords.GetHighScore(selectedSong);
                float highScore = Convert.ToSingle(highScoreInfo.score);
                float highScorePercentage = GetHighScorePercentage(selectedSong);

                string highScoreString = "<size=" + inGameHighScoreSize + ">" + String.Format("{0:n0}", highScore).Replace(",", " ") + "</size>";
                string highScorePercentageString = "<size=" + inGamePercentSize + "> (" + String.Format("{0:0.00}", highScorePercentage) + "%)</size>";

                scoreKeeperDisplay.highScoreDisplay.text = "<size=" + inGameHighScoreLabelSize + ">" + inGameHighScoreLabelText + "</size>" + highScoreString + highScorePercentageString;
            }
        }

        public static void SetData(LeaderboardRow leaderboardRow)
        {

            UpdateLeaderboardRow(leaderboardRow);
            //MelonCoroutines.Start(UpdateLeaderboardRowCoroutine(leaderboardRow));
        }

        //Tracking the play history SetTopScore function
        public static void SetTopScore(SongInfoTopScoreItem item)
        {
            SongInfoTopScoreItem topScoreItem = item;

            //Get percentage
            float percentage = GetHighScorePercentage(selectedSong);

            //Make pretty-ish strings
            string scoreString = "<size=" + historyTopScoreSize + ">" + String.Format("{0:n0}", HighScoreRecords.GetHighScore(selectedSong).score).Replace(",", " ") + "</size>";
            string percentageString = "<size=" + historyTopPercentSize + "> (" + String.Format("{0:0.00}", percentage) + "%)</size>";

            //Update label
            topScoreItem.score.text = scoreString + percentageString;
        }

        //Tracking the song's buttons score display update function
        public static void UpdateScoreDisplays(SongSelectItem item, int score)
        {

            //If the score is zero we don't do anything
            if (score != 0)
            {
                SongSelectItem button = item;

                //Get percentage
                float percentage = GetHighScorePercentage(button.GetSongData().songID);

                //Make pretty-ish strings
                string scoreString = "<size=" + songListHighScoreSize + ">" + String.Format("{0:n0}", score).Replace(",", " ") + "</size>";
                string percentageString = "<size=" + songListPercentSize + "> (" + String.Format("{0:0.00}", percentage) + "%)</size>";

                //Update button
                button.highScoreLabel.text = scoreString + percentageString;
            }
        }

        //Tracking selected song
        public static void OnSelect(SongSelectItem ssi)
        {

            SongSelectItem button = ssi;
            string songID = button.mSongData.songID;

            selectedSong = songID;
            MelonLogger.Log(songID);
        }

        public override void OnUpdate()
        {
            //Tracking menu state
            menuState = MenuState.GetState();

            //If menu changes
            if (menuState != oldMenuState)
            {
                //Updating state
                oldMenuState = menuState;

                //Put stuff to do when a menu change triggers here

                //Doing this in an effort to call this the less often possible.
                //It doesn't work at boot so going it at a menu change is reasonable I guess
                starThresholds = UnityEngine.Object.FindObjectOfType<StarThresholds>();
            }
        }

    }
}



