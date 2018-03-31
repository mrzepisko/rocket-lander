using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
namespace RocketLander {
    public static class PersistentData {
        public static int Landed { get { return score.Landed; } }
        public static int Total { get { return score.Total; } }

        public static void AddAttempt() {
            score.AddAttempt();
        }

        public static void AddLanded() {
            score.AddLanded();
        }

        public static void Wipe() {
            score.Wipe();
        }

        static ScoreManager score = new ScoreManagerPrefs();


        /// <summary>
        /// Simple save implementation using Unity built-in PlayerPrefs.
        /// </summary>
        class ScoreManagerPrefs : ScoreManager {
            const string KEY_LANDED = "score_landed", KEY_TOTAL = "score_total";

            public int Landed { get { return PlayerPrefs.GetInt(KEY_LANDED, 0); } }
            public int Total { get { return PlayerPrefs.GetInt(KEY_TOTAL, 0); } }

            public void AddAttempt() {
                PlayerPrefs.SetInt(KEY_TOTAL, Total + 1);
                PlayerPrefs.Save();
            }

            public void AddLanded() {
                PlayerPrefs.SetInt(KEY_LANDED, Landed + 1);
                PlayerPrefs.Save();
            }

            public void Wipe() {
                PlayerPrefs.SetInt(KEY_TOTAL, 0);
                PlayerPrefs.SetInt(KEY_LANDED, 0);
                PlayerPrefs.Save();
            }
            #region TODO
            /// <summary>
            /// PlayerPrefs was going crazy in recent history, file save is more reliable. TODO
            /// </summary>
            class ScoreManagerFile : ScoreManager {
                public int Landed {
                    get {
                        throw new System.NotImplementedException();
                    }
                }

                public int Total {
                    get {
                        throw new System.NotImplementedException();
                    }
                }

                public void AddAttempt() {
                    throw new System.NotImplementedException();
                }

                public void AddLanded() {
                    throw new System.NotImplementedException();
                }

                public void Wipe() {
                    throw new System.NotImplementedException();
                }
            }
            #endregion
        }

        interface ScoreManager {
            int Landed { get; }
            int Total { get; }

            void AddAttempt();
            void AddLanded();
            void Wipe();
        }
    }
}