package com.bytebridge.backend.Leaderboard;


    public class LeaderboardDTO {
        private String nic;
        private int kills;
        private int waves;

        public String getNic() {
            return nic;
        }

        public void setNic(String nic) {
            this.nic = nic;
        }

        public int getKills() {
            return kills;
        }

        public void setKills(int kills) {
            this.kills = kills;
        }

        public int getWaves() {
            return waves;
        }

        public LeaderboardDTO(String nic, int kills, int waves) {
            this.nic = nic;
            this.kills = kills;
            this.waves = waves;
        }

        public void setWaves(int waves) {
            this.waves = waves;
        }
    }
