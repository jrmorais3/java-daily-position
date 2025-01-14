package com.finance.dailyposition.jpa.entities;

import jakarta.persistence.*;
import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;
import java.time.LocalDate;

@Entity
@Getter
@Setter
public class FinancialEntry {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false)
    @NotBlank(message = "Category cannot be blank")
    private String category;

    @Column(nullable = false)
    @NotBlank(message = "Description cannot be blank")
    private String description;

    @Column(name = "amount", nullable = false, precision = 10, scale = 2) // Rename the column
    @DecimalMin(value = "0.01", message = "Value must be greater than 0")
    private BigDecimal value;

    @Column(name = "financial_date", nullable = false)
    @NotNull(message = "Financial date cannot be null")
    private LocalDate financialDate;

    @ManyToOne
    @JoinColumn(name = "account_id")
    private Account account;
}
