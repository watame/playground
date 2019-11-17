/**
 * @file read_binary_sample.c
 * @brief バイナリ読み込みサンプル関数
 */
#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <string.h>
#include <read_binary_sample.h>

/**
 * @brief ファイルポインタとサイズを元に仕様で定義されたバイナリファイルを読み込む
 * @param[in] (fp) 展開されたファイルのファイルポインタ
 * @param[in] (dump_size) データ格納範囲
 * @return なし
 */
void ReadBinaryTest( FILE *fp, long dump_size ) {
	/* 念の為1バイトの余剰を加えてメモリ領域を確保する */
	char *top = (char*) malloc( dump_size + 1 );

	if( NULL == top ) {
		printf( "Error: %s\n", strerror( errno ) );
		return;
	}

	/* 指定されたサイズ分のメモリ領域を「1回」読み込んで、ヘッダ構造体に格納する */
	if( fread( top, dump_size, 1, fp ) < 1 ) {
		if( top != NULL ) {
			free( top );
		}
		printf( "Error: %s\n", strerror( errno ) );
		return;
	}

	while( fread( top, dump_size, 1, fp ) > 0 ) {

		/* ポインタにHeader領域分のメモリ情報を設定する */
		HEADER *header = NULL;
		header = (HEADER*) top;

		/* 読み込み領域保持用のポインタ（Header領域分を進めた状態とする） */
		int work_offset = sizeof(HEADER);
		char *p = ( top + work_offset );

		/* 各変数のフラグを見て値を格納する */
		int int_val = header->comment_count;
		printf( "comment_count:%d\n", int_val );

		/* ポインタにOption領域分のメモリ情報を設定する */
		OPTION *option = NULL;
		option = (OPTION*) header->option;
		memcpy( &option, header->option, sizeof(OPTION) );
		printf( "option_com_exist:%d\n", option->comment_exist );

		if( 1 == option->comment_exist ) {
			/* コメント数分の可変のフィールドの構造体ポインタを宣言 */
			COMMENT *comment[header->comment_count];

			/* ループで値を格納 */
			int i = 0;
			for( i = 0; i < header->comment_count; i++ ) {
				comment[i] = (COMMENT*) p;
				printf( "com_id:%d\n", comment[i]->comment_id );
				printf( "comment:%s\n", comment[i]->comment );
				/* 格納した領域分保持しているポインタのアドレスをズラす */
				work_offset += sizeof(COMMENT);
				p = top + work_offset;
			}
		}
	}

	/* 保持していたバイナリ読み込み情報の解放 */
	free( top );
}

/**
 * @brief エントリポイント
 * @return 正常終了コード（0）
 */
int main( void ) {
	/* バイナリでファイル展開 */
	char path[] = "/xxx/yyy/zzz.dat";
	FILE *fp = fopen( path, "rb" );
	/* 読み込むバッファ領域を定義 */
	int buffer_size = 1024;
	ReadBinaryTest( fp, buffer_size );
	fclose( fp );
	return 0;
}
