package com.finance.dailyposition.jpa.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.io.Serializable;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class OperationBankDto implements Serializable {
    private Long id;

    private String description;

    private CategoryDto category;
}