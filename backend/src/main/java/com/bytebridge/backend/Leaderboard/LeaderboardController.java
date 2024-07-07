package com.bytebridge.backend.Leaderboard;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("api/leaderboard")
public class LeaderboardController {
    private final LeaderboardService leaderboardService;

    @Autowired
    public LeaderboardController(LeaderboardService leaderboardService) {
        this.leaderboardService = leaderboardService;
    }

    @CrossOrigin(origins = "*")
    @GetMapping
    public List<LeaderboardDTO> getLeaderboard() {
        return leaderboardService.getAllLeaderboard();
    }

    @CrossOrigin(origins = "*")
    @PostMapping("/create")
    public Leaderboard createLeaderboard(@RequestBody LeaderboardDTO leaderboardDTO) {
        return leaderboardService.saveOrUpdateLeaderboard(leaderboardDTO);
    }
}