package com.bytebridge.backend.Questionnaire;

import java.util.List;

import jakarta.persistence.CollectionTable;
import jakarta.persistence.Column;
import jakarta.persistence.ElementCollection;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.SequenceGenerator;
import jakarta.persistence.Table;

@Entity
@Table
public class Question {

    @Id
    @SequenceGenerator(name = "question_sequence", sequenceName = "question_sequence", allocationSize = 1)
    @GeneratedValue(generator = "question_sequence", strategy = GenerationType.SEQUENCE)
    private Long id;

    @Column(columnDefinition = "TEXT")
    private String question;

    @ElementCollection
    @CollectionTable(name = "answers", joinColumns = @JoinColumn(name = "question_id"))
    @Column(name = "answer", length = 1000) // Increase length as needed
    private List<String> answers;

    private int correctAnswerIndex;
    private Integer answeredIndex; // Use Integer to allow null values

    @Column(columnDefinition = "TEXT")
    private String genericFeedback;

    // Constructor
    public Question() {
    }

    public Question(Long id, String question, List<String> answers, int correctAnswerIndex,
            Integer answeredIndex,
            String genericFeedback) {
        this.id = id;
        this.question = question;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
        this.answeredIndex = answeredIndex;
        this.genericFeedback = genericFeedback;
    }

    public Question(String question, List<String> answers, int correctAnswerIndex, Integer answeredIndex,
            String genericFeedback) {
        this.question = question;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
        this.answeredIndex = answeredIndex;
        this.genericFeedback = genericFeedback;
    }

    // Getters and Setters
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getQuestion() {
        return question;
    }

    public void setQuestion(String question) {
        this.question = question;
    }

    public List<String> getAnswers() {
        return answers;
    }

    public void setAnswers(List<String> answers) {
        this.answers = answers;
    }

    public int getCorrectAnswerIndex() {
        return correctAnswerIndex;
    }

    public void setCorrectAnswerIndex(int correctAnswerIndex) {
        this.correctAnswerIndex = correctAnswerIndex;
    }

    public Integer getAnsweredIndex() {
        return answeredIndex;
    }

    public void setAnsweredIndex(Integer answeredIndex) {
        this.answeredIndex = answeredIndex;
    }

    public String getGenericFeedback() {
        return genericFeedback;
    }

    public void setGenericFeedback(String genericFeedback) {
        this.genericFeedback = genericFeedback;
    }
}