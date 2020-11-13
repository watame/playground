fn main() {
    println!("start string_memo!");
    str_string_memo();
    println!("start tuple_memo!");
    tuple_memo();
    println!("start array_memo!");
    array_memo();
    println!("start struct_memo!");
    struct_memo();
    println!("start enum_memo!");
    enum_memo();
    println!("start option_memo!");
    option_memo();
    println!("start result_memo!");
    result_memo();
}

fn str_string_memo() {
    // strはメモリ上の「文字列」データのスタート地点、長さを保持している（スライス）
    // [slice] https://doc.rust-lang.org/std/slice/index.html
    // -> 長さを保持している都合上、文字列を予め定義する必要がある（固定サイズ出ないとダメなので）
    //    固定文字列の参照として取得する必要がある
    let string_slice: &str = "this is string slice.";
    // Stringは変更可能な文字列（サイズ変更可能）な型
    // [String] https://doc.rust-lang.org/std/string/struct.String.html
    // &str.to_string()をした時点でstrで参照している文字列をStringインスタンスとして生成
    // ※この時点でメモリに配置されるので、カジュアルに行いすぎるとメモリを圧迫する
    let slice_to_string: String = string_slice.to_string();
    println!("{}", slice_to_string);

    // String->strのやり方
    let string_data: String = String::from("this is string.");
    // &strとしてStringの参照を定義する
    let string_to_slice: &str = &string_data;
    println!("{}", string_to_slice);
}

fn tuple_memo() {
    // 異なる型をまとめて保持することのできる型
    // 関数が複数の値を戻したい時に利用する
    // https://doc.rust-jp.rs/rust-by-example-ja/primitives/tuples.html
    let tuple_data = (1, "this is string data");
    // 変数.x の形式で内部の値にアクセスできる
    println!("tuple data 0: {}", tuple_data.0);
    println!("tuple data 1: {}", tuple_data.1);
    println!("tuple data: {:?}", tuple_data);

    // 同一型で値を変更してみる
    let mut mutabule_tuple_data = (1, "this is string data");
    mutabule_tuple_data.0 = 2;
    mutabule_tuple_data.1 = "changed!";
    println!("mutable tuple data: {:?}", mutabule_tuple_data);
}

fn array_memo() {
    // 特定の型を連続で収めた集合、要素数は決定している必要がある
    // [array] https://doc.rust-lang.org/std/primitive.array.html

    // 全て同一の値で初期化
    let same_array_data: [&str; 3] = ["initial_string"; 3];
    println!("same array data: {:?}", same_array_data);
    // 個々で要素を入れて初期化
    let mut mutable_array_data: [i32; 3] = [0, 1, 2];
    println!("before mutable array data: {:?}", mutable_array_data);
    mutable_array_data[1] = 10;
    mutable_array_data[2] = 20;
    println!("after mutable array data: {:?}", mutable_array_data);
}

// Debugアノテーションを付与することで'{:?}'で表示できるようになる
// https://qiita.com/Kashiwara/items/9cec0d4940a8c92e85a5
#[derive(Debug)]
// structで構造体が作成できる（クラスのようなもの？）
struct TestStruct {
    text: String,
    test_num: u32,
}

fn struct_memo() {
    let test1 = TestStruct {
        text: String::from("test1"),
        test_num: 1,
    };
    println!("test1 data: {:?}", test1);

    let test2 = TestStruct {
        text: String::from("test2"),
        test_num: 2,
    };
    println!("test1 data: {:?}", test2);
}

// enumで列挙型が作成可能
// 列挙体は数値だけではなく、structの定義を満たすものならば列挙体要素の型としてい指定できる
// https://doc.rust-jp.rs/rust-by-example-ja/custom_types/enum.html
// 利用方法の参考
// https://qiita.com/deta-mamoru/items/9b7c2616dd21f8224617
#[derive(Debug)]
enum TestEnum {
    // 特に指定なし
    NoTypeDef,
    // structで定義されている型を指定
    TypeStr(String),
    // structで定義されている型、および、それを紐づける変数名を指定
    NamedType { name: String, value: u32 },
}

fn enum_memo() {
    // 各種列挙体の定義毎に値を指定する
    let no_type_def = TestEnum::NoTypeDef;
    let type_str = TestEnum::TypeStr(String::from("this is string value."));
    let named_type = TestEnum::NamedType {
        name: String::from("this is name."),
        value: 10,
    };

    // #[derive(Debug)]を付与しているので、Debugプリント'{:?}'が出来る
    println!("no_type_def: {:?}", no_type_def);
    println!("type_str: {:?}", type_str);
    println!("named_type: {:?}", named_type);
}

// 引数が0以外の場合にSomeに値を入れたOption列挙体を戻す
fn return_option(some_val: u32) -> Option<u32> {
    if some_val == 0{
        None
    } else {
        Some(some_val)
    }
}

fn option_memo() {
    // Optionは「値がない」状態の'None'と、「値が格納されている」'Some'を表現できる列挙体
    // パターンマッチング処理で利用されることが多い
    // https://doc.rust-lang.org/std/option/index.html
    let result_none = return_option(0);
    println!("result_none: {:?}", result_none);

    let result_some = return_option(10);
    println!("result_some: {:?}", result_some);
}


fn result_memo(){
    // Resultは「正常値」の'Ok'と、「エラー値」の'Err'を表現できる列挙体
    // パターンマッチングでエラー処理をすることが多い

    // OKの場合の列挙体
    let result_1: Result<u32, String> = Ok(1);
    // Errの場合の列挙体
    let result_2: Result<u32, String> = Err(String::from("error!"));

    // Resultの値によって表示を変更する方法はmatch, if letの2パターンで処理するのが常套
    // [if let] https://doc.rust-jp.rs/rust-by-example-ja/flow_control/if_let.html
    if let Ok(val) = result_1 {
        println!("val: {}", val);
    }
    if let Ok(val) = result_2 {
        println!("this block not execute;");
    }

    // matchは全て可能な値をカバーしないとダメ
    // [match] https://doc.rust-jp.rs/rust-by-example-ja/flow_control/match.html
    match result_1 {
        Ok(val) => println!("val: {}", val),
        Err(err) => println!("err: {}", err),
    }
    match result_2 {
        Ok(val) => println!("val: {}", val),
        Err(err) => println!("err: {}", err),
    }
}