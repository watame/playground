use csv::ReaderBuilder;
use serde::Deserialize;
use std::cmp::PartialEq;
use std::error::Error;
use std::path::Path;

/// annual-enterprise-survey-2019-financial-year-provisional-size-bands-csv読み込み用の構造体
/// フィールドはcsvで定義されているカラム名をそのまま利用して定義
/// PartialEqによる比較
///     https://stackoverflow.com/questions/26958178/how-do-i-automatically-implement-comparison-for-structs-with-floats-in-rust
#[derive(Debug, Deserialize, PartialEq)]
#[allow(non_snake_case)]
struct Survey {
    year: i32,
    industry_code_ANZSIC: String,
    industry_name_ANZSIC: String,
    rme_size_grp: String,
    variable: String,
    value: String,
    unit: String,
}

/// CSV読み込みとパースを行う
fn target_csv_to_parse_struct(target_file_path: &Path) -> Result<Vec<Survey>, Box<dyn Error>> {
    // 引数のパスを読み込みデリミタ','で分割したストリームを取得
    let mut reader = ReaderBuilder::new()
        .delimiter(b',')
        .from_path(target_file_path)?;

    let mut survey_list: Vec<Survey> = Vec::new();
    // データを1行ずつ読み込み、structにパースする
    for result in reader.deserialize() {
        let record: Survey = result?;
        //        println!("{:?}", record);
        survey_list.push(record);
    }

    Ok(survey_list)
}

fn main() -> Result<(), Box<dyn Error>> {
    const FILE_NAME: &str =
        "annual-enterprise-survey-2019-financial-year-provisional-size-bands-csv.csv";
    let target_path = Path::new("./rsc/").join(FILE_NAME);

    // csvファイルの読み込み
    let mut survey_list_sort_by = target_csv_to_parse_struct(&target_path)?;
    survey_list_sort_by.sort_by(|a, b| {
        a.value
            .cmp(&b.value)
            .then(a.variable.cmp(&b.variable).then(a.unit.cmp(&b.unit)))
    });

    // csvファイルの読み込み
    let mut survey_list_is_sorted_by = target_csv_to_parse_struct(&target_path)?;
    survey_list_is_sorted_by.sort_by_key(|a| {
        // タプルで比較順を指定する
        // 参考：https://users.rust-lang.org/t/solved-advanced-sort-on-vector-of-slice/13477/9
        (
            a.value.to_string(),
            a.variable.to_string(),
            a.unit.to_string(),
        )
    });

    // sort_by, sort_by_keyで同一の並びになっているか確認
    for record in survey_list_sort_by
        .iter()
        .zip(survey_list_is_sorted_by.iter())
    {
        if record.0 == record.1 {
            continue;
        }
        println!("{:?}", record);
    }

    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_struct_is_equal_normal() {
        // 比較用Struct
        let survey_a = Survey {
            year: 1,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "JKL".to_string(),
            value: "MNO".to_string(),
            unit: "PQR".to_string(),
        };

        // survey_aと同内容のデータを保持しているStruct
        let survey_b = Survey {
            year: 1,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "JKL".to_string(),
            value: "MNO".to_string(),
            unit: "PQR".to_string(),
        };
        assert!(survey_a == survey_b);
    }

    #[test]
    fn test_struct_is_equal_error() {
        // 比較用Struct
        let survey_a = Survey {
            year: 1,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "JKL".to_string(),
            value: "MNO".to_string(),
            unit: "PQR".to_string(),
        };

        // survey_aとyearのみが異なるStruct
        let diff_year = Survey {
            year: 2,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "JKL".to_string(),
            value: "MNO".to_string(),
            unit: "PQR".to_string(),
        };

        // survey_aとindustry_codeのみが異なるStruct
        let diff_code = Survey {
            year: 1,
            industry_code_ANZSIC: "XYZ".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "JKL".to_string(),
            value: "MNO".to_string(),
            unit: "PQR".to_string(),
        };

        // survey_aとindustry_nameのみが異なるStruct
        let diff_name = Survey {
            year: 1,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "XYZ".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "JKL".to_string(),
            value: "MNO".to_string(),
            unit: "PQR".to_string(),
        };

        // survey_aとsize_grpのみが異なるStruct
        let diff_size = Survey {
            year: 1,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "XYZ".to_string(),
            variable: "JKL".to_string(),
            value: "MNO".to_string(),
            unit: "PQR".to_string(),
        };

        // survey_aとvariableのみが異なるStruct
        let diff_variable = Survey {
            year: 1,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "XYZ".to_string(),
            value: "MNO".to_string(),
            unit: "PQR".to_string(),
        };

        // survey_aとvalueのみが異なるStruct
        let diff_value = Survey {
            year: 1,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "JKL".to_string(),
            value: "XYZ".to_string(),
            unit: "PQR".to_string(),
        };

        // survey_aとunitのみが異なるStruct
        let diff_unit = Survey {
            year: 1,
            industry_code_ANZSIC: "ABC".to_string(),
            industry_name_ANZSIC: "DEF".to_string(),
            rme_size_grp: "GHI".to_string(),
            variable: "JKL".to_string(),
            value: "MNO".to_string(),
            unit: "XYZ".to_string(),
        };

        // 全てが異なるstruct
        let diff_all = Survey {
            year: 2,
            industry_code_ANZSIC: "abc".to_string(),
            industry_name_ANZSIC: "def".to_string(),
            rme_size_grp: "ghi".to_string(),
            variable: "jkl".to_string(),
            value: "mno".to_string(),
            unit: "pqr".to_string(),
        };

        assert_eq!(false, survey_a == diff_year);
        assert_eq!(false, survey_a == diff_code);
        assert_eq!(false, survey_a == diff_name);
        assert_eq!(false, survey_a == diff_size);
        assert_eq!(false, survey_a == diff_variable);
        assert_eq!(false, survey_a == diff_value);
        assert_eq!(false, survey_a == diff_unit);
        assert_eq!(false, survey_a == diff_all);
    }
}
