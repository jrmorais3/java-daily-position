package com.finance.dailyposition.service;

import com.finance.dailyposition.jpa.repositories.OperationBankRepository;
import org.springframework.stereotype.Service;

@Service
public class OperationBankService {

    private final OperationBankRepository operationBankRepository;

    public OperationBankService(OperationBankRepository operationBankRepository) {
        this.operationBankRepository = operationBankRepository;
    }
}
