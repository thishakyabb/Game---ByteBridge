package com.bytebridge.backend.Leaderboard;
import ch.qos.logback.core.joran.conditional.IfAction;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.bytebridge.backend.Profile.Profile;
import com.bytebridge.backend.Profile.ProfileRepository;

import java.util.List;
import java.util.Optional;

@Service
public class LeaderboardService {
    
    private final LeaderboardRepository leaderboardRepository;
    private final ProfileRepository profileRepository;
    @Autowired
    public LeaderboardService(LeaderboardRepository leaderboardRepository, ProfileRepository profileRepository) {
        this.leaderboardRepository = leaderboardRepository;
        this.profileRepository = profileRepository;
    }
    public List<LeaderboardDTO> getAllLeaderboard() {
        return leaderboardRepository.findAllLeaderboardData();
    }
    public Leaderboard saveOrUpdateLeaderboard(LeaderboardDTO leaderboardDTO) {
        Optional<Profile> profileOptional = Optional.ofNullable(profileRepository.findByNic(leaderboardDTO.getNic()));
        if (profileOptional.isPresent()) {
            Profile profile = profileOptional.get();
            Leaderboard leaderboard = leaderboardRepository.findByProfileNic(profile.getNic());
            if (leaderboard != null) {
                // Update the existing leaderboard with new data
                if (leaderboard.getKills()* leaderboard.getWaves() < leaderboardDTO.getKills()*leaderboardDTO.getWaves()) {
                // basically acts as a point system to determine If this entry is better than the last one
                    leaderboard.setKills(leaderboardDTO.getKills());
                    leaderboard.setWaves(leaderboardDTO.getWaves());
                }
            } else {
                // Create a new leaderboard entry
                leaderboard = new Leaderboard(profile, leaderboardDTO.getKills(), leaderboardDTO.getWaves());
            }
            return leaderboardRepository.save(leaderboard);
        } else {
            return null;
        }
    } 
}


