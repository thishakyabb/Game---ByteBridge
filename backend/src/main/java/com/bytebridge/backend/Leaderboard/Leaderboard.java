package com.bytebridge.backend.Leaderboard;

import com.bytebridge.backend.Profile.Profile;
import jakarta.persistence.*;

@Entity
@Table(name = "leaderboard")
public class Leaderboard {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name = "profile_id")
    private Profile profile;

    private int kills;
    private int waves;

    public Leaderboard() {
        // Default constructor required by JPA
    }

    public Leaderboard(Profile profile, int kills, int waves) {
        this.profile = profile;
        this.kills = kills;
        this.waves = waves;
    }

    // Getters and setters
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Profile getProfile() {
        return profile;
    }

    public void setProfile(Profile profile) {
        this.profile = profile;
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

    public void setWaves(int waves) {
        this.waves = waves;
    }
}
