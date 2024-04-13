package com.bytebridge.backend.Profile;

import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.bytebridge.backend.Questionnaire.Question;

import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

@RestController
@RequestMapping(path = "api/profiles")
public class ProfileController {

    private final ProfileService profileService;

    @Autowired
    public ProfileController(ProfileService profileService) {
        this.profileService = profileService;
    }

    @CrossOrigin(origins = "*")
    @PostMapping("/create")
    public ResponseEntity<?> createProfile(@RequestBody Profile profile) {
        Optional<Profile> existingProfile = Optional.ofNullable(profileService.getProfile(profile.getNic()));
        if (existingProfile.isPresent()) {
            return new ResponseEntity<>("User with NIC " + profile.getNic() + " already exists",
                    HttpStatus.BAD_REQUEST);
        }
        int marks = 0;
        for (Question question : profile.getQuestions()) {
            if (question.getAnsweredIndex() != null
                    && question.getAnsweredIndex().equals(question.getCorrectAnswerIndex())) {
                marks += 1;
            }
        }
        profile.setMarks(marks);
        profileService.createProfile(profile);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @CrossOrigin(origins = "*")
    @GetMapping("/{nic}")
    public Profile getProfile(@PathVariable String nic) {

        return profileService.getProfile(nic);
    }

    @CrossOrigin(origins = "*")
    @GetMapping("/authorizedforquestionnaire/{nic}")
    public boolean isAuthorizedForQuestionnaire(@PathVariable String nic) {
        return profileService.isAuthorizedForQuestionnaire(nic);
    }
    // Add your endpoints here
}