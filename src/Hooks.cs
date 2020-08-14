using Harmony;
using System;

namespace AudicaModding
{
    internal static class Hooks
    {
        [HarmonyPatch(typeof(SongSelectItem), "OnSelect")]
        private static class PatchSongOnSelect
        {
            private static void Postfix(SongSelectItem __instance)
            {
                AudicaMod.OnSelect(__instance);
            }
        }

        [HarmonyPatch(typeof(SongSelectItem), "UpdateScoreDisplays", new Type[] { typeof(int), typeof(KataConfig.Difficulty), typeof(float), typeof(bool) })]
        private static class PatchSongUpdateScore
        {
            private static void Postfix(SongSelectItem __instance, int score, KataConfig.Difficulty difficulty, float percent, bool fullCombo)
            {
                AudicaMod.UpdateScoreDisplays(__instance, score);
            }
        }

        /*
        [HarmonyPatch(typeof(LeaderboardRow), "SetData", new Type[] { typeof(LeaderboardRowData), typeof(int), typeof(int) })]
        private static class PatchLeaderboardSetData
        {
            private static void Prefix(LeaderboardRow __instance, LeaderboardRowData row, int displayRank, int totalLeaderboardEntries)
            {
                //AudicaMod.SetData(__instance);
            }
        }
        */

        [HarmonyPatch(typeof(ScoreKeeperDisplay), "Update")]
        private static class PatchScoreKeeperUpdate
        {
            private static void Postfix(ScoreKeeperDisplay __instance)
            {
                AudicaMod.ScoreKeeperDisplayUpdate(__instance);
            }
        }

        [HarmonyPatch(typeof(SongInfoPanel), "SetTopScore", new Type[] { typeof(HighScoreRecords.HighScoreInfo), typeof(SongInfoTopScoreItem) })]
        private static class PatchSetTopScore
        {
            private static void Postfix(SongInfoPanel __instance, ref SongInfoTopScoreItem item)
            {
                AudicaMod.SetTopScore(item);
            }
        }

        [HarmonyPatch(typeof(SongInfoPanel), "OnEnable")]
        private static class PatchSongInfoOnEnable
        {
            private static void Postfix()
            {
                //return true;
            }
        }
    }
}