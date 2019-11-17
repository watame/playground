/**
 * @file read_binary_sample.h
 * @brief バイナリ読み込みサンプル関数
 */

/* 多重読み込み防止 */
#pragma once
/* 構造体にパディングが入らない設定を付与 */
#pragma pack(1)

/* (2byte)格納されている値はマイナスに成り得ないため、unsigned shortで定義 */
typedef unsigned short USHORT;
/* (4byte)格納されている値はマイナスに成り得ないため、unsigned intで定義 */
typedef unsigned int UINT;
/* 1バイトの大きさが欲しいだけなので、unsigned charで定義 */
typedef unsigned char BYTE;

/**
 * ヘッダ領域で必ず格納されている情報の定義
 */
typedef struct {
	USHORT comment_count; /*コメント数*/
	BYTE option[2]; /*optionフィールド(ビット保持系はunsigned char)*/
} HEADER;

/**
 * comment領域の定義
 */
typedef struct {
	UINT comment_id; /*コメントID*/
	char comment[256]; /*コメント内容*/
} COMMENT;

/**
 * optionのビットフィールド定義
 */
typedef struct {
	unsigned short reserved :15; /*未使用*/
	unsigned short comment_exist :1; /*comment存在フラグ（0：存在しない / 1：存在する）*/
} OPTION;

/* 構造体にパディングが入らない設定を解除 */
#pragma pack()