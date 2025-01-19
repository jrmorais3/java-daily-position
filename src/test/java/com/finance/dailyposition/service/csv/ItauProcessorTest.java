package com.finance.dailyposition.service.csv;

import com.finance.dailyposition.jpa.repositories.AccountRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.web.multipart.MultipartFile;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class ItauProcessorTest {

    private ItauProcessor itauFileProcessor;
    private AccountCsvProcessor accountCsvProcessor;

    @Mock
    private AccountRepository accountRepository;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
        accountCsvProcessor = new AccountCsvProcessor();
        itauFileProcessor = new ItauProcessor(accountRepository, accountCsvProcessor);
    }

    @Test
    public void testProcessFile_ValidCsv() throws IOException {
        //Arrange
        String csvContent = """
                ,,,,,,,
                ,Nome:�BEACH PARK HOTEIS TURISMO S A,,,,Ag�ncia/Conta:�3827/11172-9,,
                ,Data:�14/01/2025,,,,Hor�rio:�09:04:07,,
                ,,,,,,,
                ,Extrato de 11/01/2025 at� 14/01/2025,,,,,,
                ,,,,,,,
                """;
        MultipartFile file = mock(MultipartFile.class);
        InputStream inputStream = new ByteArrayInputStream(csvContent.getBytes());
        when(file.getInputStream()).thenReturn(inputStream);

        //Act
        itauFileProcessor.processFile(file);

        //Assert
    }
}