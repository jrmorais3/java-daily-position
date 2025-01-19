package com.finance.dailyposition.utils;

import java.util.Arrays;

public final class CsvUtils {

    private CsvUtils() {}

    public static String[] removeEmptyColumns(String[] row) {
        return Arrays.stream(row)
                .filter(column -> column != null && !column.trim().isEmpty())
                .toArray(String[]::new);
    }

}
