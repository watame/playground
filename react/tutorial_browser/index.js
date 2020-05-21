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
  // コンポーネントが何かを「覚える」ためにはstateを利用する
  constructor(props) {
    // JavaScriptのクラスでは、サブクラスのコンストラクタ定義する際は常にsuperがいる
    // Reactのクラスコンポーネントでは、全コンストラクタをsuper(props)から始める
    super(props);
    // プライベートなので、子コンポーネントから変更することはできない
    this.state = {
      // 盤面をnullで埋める 
      squares: Array(9).fill(null),
      xIsNext: true,
    };
  }
  
  // クリック時のイベント定義
  handleClick(i) {
    // 配列をコピー
    const squares = this.state.squares.slice();
    // 処理が不要になったパターン(勝者が決まった or すでにマークが入っている)の場合は、何もしない
    if (calculateWinner(squares) || squares[i]){
      return;
    }
    // コピーした配列の中身をプレーヤに応じて変更
    squares[i] = this.state.xIsNext ? 'X' : 'O';
    // コンポーネントの再描画をする
    this.setState({
      // 状態を更新した配列をstateの配列に代入
      squares: squares,
      // プレーヤの手番を決めるフラグを反転させる
      xIsNext: !this.state.xIsNext,
    });
  }
  
  // Squareコンポーネント呼び出し、マス目を描画する
  renderSquare(i) {
    // stateの値をpropsとしてコンポーネントに渡す
    return (<Square
              value={this.state.squares[i]}
              // onClickをpropsとして渡している
              onClick={() => this.handleClick(i)}
              />
           );
  }

  render() {
    // 勝利条件を満たしていないか判定
    const winner = calculateWinner(this.state.squares);
    let status;
    if (winner){
      // 勝者がいる場合、勝者を表示する
      status = 'Winner: ' + winner;
    } else {
      // 勝者がいない場合、次プレーヤを表示する
      status = 'Next player: ' + (this.state.xIsNext ? 'X' : 'O');
    }

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
  render() {
    return (
      <div className="game">
        <div className="game-board">
          <Board />
        </div>
        <div className="game-info">
          <div>{/* status */}</div>
          <ol>{/* TODO */}</ol>
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