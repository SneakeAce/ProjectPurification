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

        // ��������� CSV-���� �� ������
        string[] lines = csvFile.text.Split('\n');

        if (lines.Length < 2)
        {
            Debug.LogError("CSV ���� ������ ��� �������� ������������ ������.");
            return coefficients;
        }

        // ������ ������ �������� ��������� (���������� �)
        string[] headers = lines[0].Split(';'); // ������������, ��� ����������� - ����� � ������� (;)

        // ������������ ������ ������, ������� �� ������
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue; // ���������� ������ ������

            string[] cells = line.Split(';');

            // ������ ������� - ArmorType
            if (!Enum.TryParse(cells[0], out ArmorType armorType))
            {
                Debug.LogWarning($"�� ������� ���������� ArmorType: {cells[0]}");
                continue;
            }

            // ������������ ��������� ������� (AttackType -> Coefficient)
            for (int j = 1; j < cells.Length; j++)
            {
                string header = headers[j].Trim();
                string valueText = cells[j].Trim();

                // ����������� ��������� � AttackType
                if (!Enum.TryParse(header, out AttackType attackType))
                {
                    Debug.LogWarning($"�� ������� ���������� AttackType: {header}");
                    continue;
                }

                // ����������� �������� �� ������ � float � ������ CultureInfo("ru-RU")
                if (float.TryParse(valueText, NumberStyles.Float, new CultureInfo("ru-RU"), out float coefficient))
                {
                    coefficients[(attackType, armorType)] = coefficient;
                }
                else
                {
                    Debug.LogWarning($"�� ������� ������������� �������� '{valueText}' � ������� '{header}' ��� {armorType}");
                }
            }
        }

        return coefficients;
    }
}
