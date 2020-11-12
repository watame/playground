fn main() {
    println!("start string_memo!");
    str_string_memo();
    println!("start tuple_memo!");
    tuple_memo();
    println!("start array_memo!");
    array_memo();
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
