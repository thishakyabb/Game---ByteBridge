package com.bytebridge.backend.Leaderboard;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface LeaderboardRepository extends JpaRepository<Leaderboard, Long> {
    // You can add custom query methods here if needed
    @Query("SELECT new com.bytebridge.backend.Leaderboard.LeaderboardDTO(l.profile.nic,l.kills,l.waves) FROM Leaderboard l")
    List<LeaderboardDTO> findAllLeaderboardData();
    
    @Query("SELECT l FROM Leaderboard l WHERE l.profile.nic = :nic")
    Leaderboard findByProfileNic(@Param("nic") String nic);;
}
