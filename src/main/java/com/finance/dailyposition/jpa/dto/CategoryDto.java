package com.finance.dailyposition.jpa.dto;

import lombok.Getter;
import lombok.Setter;

import java.io.Serializable;

@Getter
@Setter
public class CategoryDto implements Serializable {
    private Long id;

    private String tag;

    private OperationBankDto operations;
}