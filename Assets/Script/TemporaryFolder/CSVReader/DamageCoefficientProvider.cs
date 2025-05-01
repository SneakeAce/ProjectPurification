using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DamageCoefficientProvider : IDamageCoefficientProvider
{
    private const float DefaultCoefficient = 1f; 

    private readonly Dictionary<(AttackType, ArmorType), float> _coefficients;

    public DamageCoefficientProvider(TextAsset csvFile)
    {
        Debug.Log("DamageCoefficientProvider constructor called / csvFile = " + csvFile);
        _coefficients = Load(csvFile);
    }

    public float GetCoefficient(AttackType attackType, ArmorType armorType)
    {
        return _coefficients.TryGetValue((attackType, armorType), out var coef) ? coef : DefaultCoefficient;
    }

    private Dictionary<(AttackType, ArmorType), float> Load(TextAsset csvFile)
    {
        var coefficients = new Dictionary<(AttackType, ArmorType), float>();

        // Разделяем CSV-файл на строки
        string[] lines = csvFile.text.Split('\n');

        if (lines.Length < 2)
        {
            Debug.LogError("CSV файл пустой или содержит недостаточно данных.");
            return coefficients;
        }

        // Первая строка содержит заголовки (пропускаем её)
        string[] headers = lines[0].Split(';'); // Предполагаем, что разделитель - точка с запятой (;)

        // Обрабатываем каждую строку, начиная со второй
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue; // Пропускаем пустые строки

            string[] cells = line.Split(';');

            // Первый столбец - ArmorType
            if (!Enum.TryParse(cells[0], out ArmorType armorType))
            {
                Debug.LogWarning($"Не удалось распознать ArmorType: {cells[0]}");
                continue;
            }

            // Обрабатываем остальные столбцы (AttackType -> Coefficient)
            for (int j = 1; j < cells.Length; j++)
            {
                string header = headers[j].Trim();
                string valueText = cells[j].Trim();

                // Преобразуем заголовок в AttackType
                if (!Enum.TryParse(header, out AttackType attackType))
                {
                    Debug.LogWarning($"Не удалось распознать AttackType: {header}");
                    continue;
                }

                // Преобразуем значение из строки в float с учетом CultureInfo("ru-RU")
                if (float.TryParse(valueText, NumberStyles.Float, new CultureInfo("ru-RU"), out float coefficient))
                {
                    coefficients[(attackType, armorType)] = coefficient;
                }
                else
                {
                    Debug.LogWarning($"Не удалось преобразовать значение '{valueText}' в столбце '{header}' для {armorType}");
                }
            }
        }

        return coefficients;
    }
}
