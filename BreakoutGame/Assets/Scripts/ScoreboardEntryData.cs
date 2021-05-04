using System;

namespace Breakout.Scoreboards
{
    [Serializable]
    public struct ScoreboardEntryData
    {
        public string entryRank;
        public string entryName;
        public int entryScore;
    }
}