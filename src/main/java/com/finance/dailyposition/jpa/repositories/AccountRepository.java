package com.finance.dailyposition.jpa.repositories;

import com.finance.dailyposition.jpa.entities.Account;
import org.springframework.data.jpa.repository.JpaRepository;

public interface AccountRepository extends JpaRepository<Account, Long> {
}