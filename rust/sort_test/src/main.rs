use csv::ReaderBuilder;
use serde::Deserialize;
use std::error::Error;
use std::path::Path;

/// annual-enterprise-survey-2019-financial-year-provisional-size-bands-csv読み込み用の構造体
/// フィールドはcsvで定義されているカラム名をそのまま利用して定義
#[derive(Debug, Deserialize)]
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
        println!("{:?}", record);
        survey_list.push(record);
    }

    Ok(survey_list)
}

fn main() -> Result<(), Box<dyn Error>> {
    const FILE_NAME: &str =
        "annual-enterprise-survey-2019-financial-year-provisional-size-bands-csv.csv";
    let target_path = Path::new("./rsc/").join(FILE_NAME);

    // csvファイルの読み込み
    let survey_list = target_csv_to_parse_struct(&target_path);

    Ok(())
}
