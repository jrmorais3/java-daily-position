package com.finance.dailyposition.utils;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.util.List;

public final class DateUtils {

    private DateUtils() {}

    public static LocalDate parseDate(String dateString) {
        List<DateTimeFormatter> formatters = List.of(
                DateTimeFormatter.ofPattern("dd/MM/yyyy"),
                DateTimeFormatter.ofPattern("dd-MM-yyyy")
        );

        for (DateTimeFormatter formatter : formatters) {
            try {
                String formatterStringTrim = dateString.trim().strip();
                return LocalDate.parse(formatterStringTrim, formatter);
            } catch (DateTimeParseException e) {
            }
        }

        throw new RuntimeException("Unsupported date format: " + dateString);
    }
}
