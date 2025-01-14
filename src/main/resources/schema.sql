CREATE TABLE category
(
    id  BIGINT AUTO_INCREMENT PRIMARY KEY,
    tag VARCHAR(100)
);

CREATE TABLE operation_bank
(
    id          BIGINT AUTO_INCREMENT PRIMARY KEY,
    description VARCHAR(255),
    category_id BIGINT,
    CONSTRAINT fk_category FOREIGN KEY (category_id) REFERENCES category (id)
);

CREATE TABLE account
(
    id     BIGINT AUTO_INCREMENT PRIMARY KEY,
    number VARCHAR(50),
    bank   VARCHAR(100),
    account_date   DATE
);

CREATE TABLE financial_entry
(
    id          BIGINT AUTO_INCREMENT PRIMARY KEY,
    category    VARCHAR(100),
    description VARCHAR(255),
    value DOUBLE,
    financial_date        DATE,
    account_id  BIGINT,
    CONSTRAINT fk_account FOREIGN KEY (account_id) REFERENCES account (id)
);
