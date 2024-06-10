package com.bytebridge.backend.Profile;

import org.springframework.stereotype.Service;

import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;

@Service
public class ProfileService {

    private final ProfileRepository profileRepository;

    @Autowired
    public ProfileService(ProfileRepository profileRepository) {
        this.profileRepository = profileRepository;
    }

    public void createProfile(Profile profile) {
        Optional<Profile> existingProfile = Optional.ofNullable(profileRepository.findByNic(profile.getNic()));
        if (existingProfile.isPresent()) {
            throw new IllegalStateException("Profile with NIC " + profile.getNic() + " already exists");
        }
        profileRepository.save(profile);
    }

    public Profile getProfile(String nic) {
        return profileRepository.findByNic(nic);
    }

    public boolean isAuthorizedForQuestionnaire(String nic) {
        Optional<Profile> existingProfile = Optional.ofNullable(profileRepository.findByNic(nic));
        if (existingProfile.isPresent()&& existingProfile.get() != null) {

            return false;
        } else {
            return true;
        }
    }

    // Add your methods here
}