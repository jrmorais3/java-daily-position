package com.finance.dailyposition.jpa.repositories;

import com.finance.dailyposition.jpa.entities.FinancialEntry;
import org.springframework.data.jpa.repository.JpaRepository;

public interface FinancialEntryRepository extends JpaRepository<FinancialEntry, Long> {
}