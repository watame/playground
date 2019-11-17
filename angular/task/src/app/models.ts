/**
 * 作業入力データ
 * @export
 * @interface InputData
 */
export interface InputData {
  // 時刻
  time: string;
  // 天気
  weather: Weather;
  // 作業内容
  work: string;
}

/**
 * 天気
 * @export
 * @enum {number}
 */
export enum Weather {
  sunny = 1,
  rainy = 2,
  clowdy = 3,
  snow = 4
}
