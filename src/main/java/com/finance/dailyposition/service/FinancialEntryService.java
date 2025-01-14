package com.finance.dailyposition.service;

import com.finance.dailyposition.jpa.repositories.FinancialEntryRepository;
import org.springframework.stereotype.Service;

@Service
public class FinancialEntryService {

    private final FinancialEntryRepository financialEntryRepository;

    public FinancialEntryService(FinancialEntryRepository financialEntryRepository) {
        this.financialEntryRepository = financialEntryRepository;
    }
}
