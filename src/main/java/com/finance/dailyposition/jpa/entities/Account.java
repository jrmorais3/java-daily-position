package com.finance.dailyposition.jpa.entities;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDate;
import java.util.List;

@Entity
@Getter
@Setter
public class Account {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false)
    @NotBlank(message = "Number account cannot be blank")
    private String number;

    @Column(nullable = false)
    @NotBlank(message = "Bank cannot be blank")
    private String bank;

    @OneToMany(mappedBy = "account", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<FinancialEntry> financials;

    @Column(name = "account_date")
    private LocalDate accountDate;
}
