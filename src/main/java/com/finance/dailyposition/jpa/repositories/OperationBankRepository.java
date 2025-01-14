package com.finance.dailyposition.jpa.repositories;

import com.finance.dailyposition.jpa.entities.OperationBank;
import org.springframework.data.jpa.repository.JpaRepository;

public interface OperationBankRepository extends JpaRepository<OperationBank, Long> {
}