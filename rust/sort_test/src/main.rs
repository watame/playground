use csv::ReaderBuilder;
use serde::Deserialize;
use std::cmp::{Ordering, PartialEq};
use std::error::Error;
use std::path::Path;

/// sort-order-test.csv読み込み用Struct
/// フィールドはcsvで定義されているカラム名をそのまま利用して定義
/// PartialEqによる比較
///     https://stackoverflow.com/questions/26958178/how-do-i-automatically-implement-comparison-for-structs-with-floats-in-rust
#[derive(Debug, Deserialize, PartialEq)]
struct Person {
    // No
    no: i32,
    // 名前
    name: String,
    // 年齢
    age: i32,
    // 職業
    job: String,
}
impl PartialOrd for Person {
    /// Personオブジェクトの比較を行う
    /// age -> name -> job -> no の優先度で比較を行う
    ///
    /// ## Arguments
    /// * other : Personオブジェクトの参照
    fn partial_cmp(&self, other: &Person) -> Option<Ordering> {
        Some(
            self.age
                .cmp(&other.age)
                .then(self.name.cmp(&other.name))
                .then(self.job.cmp(&other.job))
                .then(self.no.cmp(&other.no)),
        )
    }
}

/// CSVファイルを読み込み、各行をTのStructにパースしたVectorを生成する
///
/// ## Arguments
/// * target_csv_file_path : 読み込み対象のCSVファイルへのフルパス
///
/// ## Example
/// ```
/// #[derive(Deserialize)] // Genericsの型として指定するStructはDeserialize必須
/// struct Person {
///     no: i32,
///     name: String,
///     age: i32,
///     job: String,
/// }
///
/// let person_list: Vec<Person> = target_csv_to_parse_struct<Person>(Path::new("./test.csv")).unwrap();
/// ```
///
/// ## Reference site
/// https://stackoverflow.com/questions/62479410/rust-how-to-restrict-type-parameters-for-derived-traits
fn target_csv_to_parse_struct<T>(target_csv_file_path: &Path) -> Result<Vec<T>, Box<dyn Error>>
where
    T: serde::de::DeserializeOwned, // Deserializeを実装している型を指定
{
    // 引数のパスを読み込みデリミタ','で分割したストリームを取得
    let mut reader = ReaderBuilder::new()
        .delimiter(b',')
        .from_path(target_csv_file_path)?;

    let mut list: Vec<T> = Vec::new();
    // データを1行ずつ読み込み、structにパースしてVectorに格納する
    for result in reader.deserialize() {
        let record: T = result?;
        list.push(record);
    }

    Ok(list)
}

fn main() -> Result<(), Box<dyn Error>> {
    const FILE_NAME: &str = "sort-order-test.csv";
    let target_path = Path::new("./rsc/").join(FILE_NAME);

    // csvファイルの読み込み
    let mut survey_list_sort_by: Vec<Person> = target_csv_to_parse_struct(&target_path)?;
    survey_list_sort_by.sort_by(|a, b| {
        a.age
            .cmp(&b.age)
            .then(a.name.cmp(&b.name))
            .then(a.job.cmp(&b.job))
            .then(a.no.cmp(&b.no))
    });

    // csvファイルの読み込み
    let mut survey_list_is_sorted_by: Vec<Person> = target_csv_to_parse_struct(&target_path)?;
    survey_list_is_sorted_by.sort_by_key(|a| {
        // タプルで比較順を指定する
        // 参考：https://users.rust-lang.org/t/solved-advanced-sort-on-vector-of-slice/13477/9
        (a.age, a.name.to_string(), a.job.to_string(), a.no)
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

    let answer_target_path = Path::new("./test/normal/sort-order-test_sorted.csv");
    let survey_list_is_sorted: Vec<Person> = target_csv_to_parse_struct(&answer_target_path)?;

    // ソート済みのCSVを読み込んだ結果とソートした結果が同一か確認
    for record in survey_list_is_sorted_by
        .iter()
        .zip(survey_list_is_sorted.iter())
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
    fn test_is_csv_sorted_by_define_priority_normal() {
        const FILE_NAME: &str = "sort-order-test_sorted.csv";
        let target_path = Path::new("./test/normal").join(FILE_NAME);

        // csvファイルの読み込み
        let person_sorted_csv: Vec<Person> = target_csv_to_parse_struct(&target_path).unwrap();

        // テスト結果格納用
        let mut result = true;
        // Window(2)を利用することで、2要素ずつのイテレータを取得する
        // 前の値も含めてforループを回す
        for person_tuple in person_sorted_csv.windows(2) {
            println!("{:?}", person_tuple);
            // 定義した優先度で各フィールドの比較処理を行い、1つ前の要素が大きければエラーとする
            if person_tuple[0] > person_tuple[1] {
                result = false;
                break;
            }
        }
        assert!(result);
    }

    #[test]
    fn test_is_csv_sorted_by_define_priority_error() {
        const FILE_NAME: &str = "sort-order-test_sorted.csv";
        let target_path = Path::new("./test/error").join(FILE_NAME);

        // csvファイルの読み込み
        let person_sorted_csv: Vec<Person> = target_csv_to_parse_struct(&target_path).unwrap();

        // テスト結果格納用
        let mut result = true;
        // Window(2)を利用することで、2要素ずつのイテレータを取得する
        // 前の値も含めてforループを回す
        for person_tuple in person_sorted_csv.windows(2) {
            println!("{:?}", person_tuple);
            // 定義した優先度で各フィールドの比較処理を行い、1つ前の要素が大きければエラーとする
            if person_tuple[0] > person_tuple[1] {
                result = false;
                break;
            }
        }
        assert_eq!(false, result);
    }
}
