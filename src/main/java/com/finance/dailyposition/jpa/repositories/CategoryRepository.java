package com.finance.dailyposition.jpa.repositories;

import com.finance.dailyposition.jpa.entities.Category;
import org.springframework.data.jpa.repository.JpaRepository;

public interface CategoryRepository extends JpaRepository<Category, Long> {
}