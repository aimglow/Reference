mytblテーブルの主キーの定義内容（対象列）を表示する
```
SELECT
  col.table_name,
  col.column_name
FROM
  USER_CONS_COLUMNS col
    INNER JOIN USER_CONSTRAINTS con
    ON col.constraint_name = con.constraint_name
WHERE
    con.table_name = 'MYTBL'
AND con.constraint_type = 'P'
ORDER BY
  col.position
	;
```

