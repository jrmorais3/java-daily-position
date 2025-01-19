package com.finance.dailyposition.jpa.dto;

import java.io.Serializable;

public class CategoryDto implements Serializable {
    private Long id;

    private String tag;

    private OperationBankDto operations;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getTag() {
        return tag;
    }

    public void setTag(String tag) {
        this.tag = tag;
    }

    public OperationBankDto getOperations() {
        return operations;
    }

    public void setOperations(OperationBankDto operations) {
        this.operations = operations;
    }
}