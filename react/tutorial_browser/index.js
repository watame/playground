// renderメソッドだけを有して、自分のstateを持たないコンポーネントを関数コンポーネントとして書くことができる
// propsを引数として、表示すべき内容を戻す関数と定義する
function Square(props) {
  return (
    // onClick={() => this.porps.onClick()}を書き換えている
    // thisはクラスではないので、不要
    // ()は省略可能
    <button className="square" onClick={props.onClick}>
      {props.value}
    </button>
  );
}

class Board extends React.Component {
  // Squareコンポーネント呼び出し、マス目を描画する
  renderSquare(i) {
    // stateの値をpropsとしてコンポーネントに渡す
    return (
      <Square
        value={this.props.squares[i]}
        // onClickをSquareコンポーネントにpropsとして渡している
        onClick={() => this.props.onClick(i)}
      />
     );
  }

  render() {
    return (
      <div>
        <div className="status">{status}</div>
        <div className="board-row">
          {this.renderSquare(0)}
          {this.renderSquare(1)}
          {this.renderSquare(2)}
        </div>
        <div className="board-row">
          {this.renderSquare(3)}
          {this.renderSquare(4)}
          {this.renderSquare(5)}
        </div>
        <div className="board-row">
          {this.renderSquare(6)}
          {this.renderSquare(7)}
          {this.renderSquare(8)}
        </div>
      </div>
    );
  }
}

class Game extends React.Component {
  // コンポーネントが何かを「覚える」ためにはstateを利用する
  constructor(props) {
    // JavaScriptのクラスでは、サブクラスのコンストラクタ定義する際は常にsuperがいる
    // Reactのクラスコンポーネントでは、全コンストラクタをsuper(props)から始める
    super(props);
    // プライベートなので、子コンポーネントから変更することはできない
    this.state = {
      // Boardコンポーネントで保持していたstateをリフトアップし、履歴形式で保持
      history: [{
        // 全盤面を保持した配列
        squares: Array(9).fill(null),
        // どのマス目に入れたか
        setSquare: null,
        // プレイヤー名
        player: null,
      }],
      // 手番
      stepNumber: 0,
      // プレイヤー手番
      xIsNext: true,
    };
  }
  
  // クリック時のイベント定義
  handleClick(i) {
    // 配列の取り出し位置(最初の配列)と、終わりの位置を指定してコピー（終わりの位置の直前の配列までがコピー対象）
    const history = this.state.history.slice(0, this.state.stepNumber + 1);
    // 現在の盤面を取得
    const current = history[history.length - 1];
    // 配列をコピー
    const squares = current.squares.slice();
    // 処理が不要になったパターン(勝者が決まった or すでにマークが入っている)の場合は、何もしない
    if (calculateWinner(squares) || squares[i]){
      return;
    }
    // コピーした盤面の中身を上書き
    squares[i] = this.state.xIsNext ? 'X' : 'O';
    // コンポーネントの再描画をする
    this.setState({
      // 状態を更新したsquareをhistryに追加した新しい配列を、stateのhistoryに代入
      // プロパティのようなオブジェクトを入れるときは{}が必要
      history: history.concat([{
        squares: squares,
        setSquare: i,
        player: squares[i],
      }]),
      // 何手番目かを保持
      stepNumber: history.length,
      // プレーヤの手番を決めるフラグを反転させる
      xIsNext: !this.state.xIsNext,
    });
  }
  
  jumpTo(step){
    this.setState({
      stepNumber: step,
      // 偶数の手番がXなのでstepが2で割り切れる場合は、trueとする 
      xIsNext: (step % 2) === 0,
    });
  }
  
  render() {
    const history = this.state.history;
    // 手番の盤面を取得
    const current = history[this.state.stepNumber];
    const winner = calculateWinner(current.squares);
    
    // タイムトラベル用のエレメント作成
    // stepにsquare、 moveにインデックスが入る
    const moves = history.map((step, move) => {
      // 保持しているすべての要素に対し、以下の処理を行う
      // 配列が0以外なら、インデックス番号をボタンに入れる
      const desc = move ?
        'Go to move #' + move :
        'Go to game start';
      // 配列が0以外なら、チェックしたボタンの位置を行列形式で取得する
      const historyInfo = move ?
        // 行を取得する際には小数点を切り捨てている
        `player: ${step.player} / (${(step.setSquare % 3) + 1}, ${Math.floor(step.setSquare / 3) + 1})` :
        '';
      return (
        /*
         リストをレンダーする際、リストの項目についてkeyプロパティを与える必要がある
         keyを与えることによって、リストのような兄弟要素の中でアイテムを特定できる
         リストが再レンダーされる際、Reactはそれぞれのリスト項目のkeyについて、
         前回のリスト項目内に同一のkeyを持つものがないかを探す

         keyの状態による挙動は以下
         * 前回リストにないkeyが含まれていれば、コンポーネントを作成
         * 前回リストにあったkeyが含まれていなければ、以前のコンポーネントを破棄
         * 前回リストと今回リストでkeyがマッチした場合、対応するコンポーネントは移動される

         ※ keyはそれぞれのコンポーネントの同一性に関する情報をReactで管理する
            -> 一意な事がわかっているので、再レンダーしてもstateが保持できるようになる
               ※ keyが変化していれば、コンポーネントは破棄されて新しいstateになる
         
         keyは特別なプロパティであり、Rectにより予約されている
         要素が作成される際、Reactはkeyプロパティを引き抜いて、戻りの要素に直接そのkeyを格納する
         どの子要素を更新すべきか決定する際にkeyを自動的に使用する
         ※ コンポーネントが自身のkeyを確認する方法はない
         
         keyが指定されなかった場合、Reactは警告を表示して、デフォルトで配列のインデックスをkeyとする
         -> 配列のインデックスをkeyとして使うことは、並び替え、挿入、削除の問題となるので、
            ※※※ 動的なリストを作る場合は、正しいkeyを割り当てることが強く推奨される ※※※
            ※ keyはコンポーネントとその兄弟(<li>の要素など)の間で一意であればOK
        */
        <li key={move}>
          <button onClick={() => this.jumpTo(move)}>{desc}</button>
          <label>{historyInfo}</label>
        </li>
      );
    });
    
    
    let status;
    if (winner) {
      status = 'Winner: ' + winner;
    } else {
      status = 'Next player: ' + (this.state.xIsNext ? 'X' : 'O');
    }
    return (
      <div className="game">
        <div className="game-board">
          <Board
            squares={current.squares}
            // onClickにhandleClickを設定し、Boardコンポーネントにpropsとして渡している
            onClick={(i) => this.handleClick(i)}
          />
        </div>
        <div className="game-info">
          <div>{status}</div>
          <ol>{moves}</ol>
        </div>
      </div>
    );
  }
}

// ========================================

ReactDOM.render(
  <Game />,
  document.getElementById('root')
);

// 勝者判定関数
function calculateWinner(squares) {
  // 縦、横、斜めで揃ったら勝ちになるラインを配列のインデックスで定義する
  const lines = [
    [0, 1, 2],
    [3, 4, 5],
    [6, 7, 8],
    [0, 3, 6],
    [1, 4, 7],
    [2, 5, 8],
    [0, 4, 8],
    [2, 4, 6],
  ];
  // 定義した勝利パターンの全配列についてループを回す
  for (let i = 0; i < lines.length; i++) {
    // 読み込んだ配列を変数に格納
    const [a, b, c] = lines[i];
    // 勝利パターンの配列全てに同一のマーク（'X' or 'O'）が入っていれば、そのマークを戻す
    if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
      return squares[a];
    }
  }
  // どちらのプレーヤも、どの勝利パターンにも当てはまらない場合はnullとする
  return null;
}