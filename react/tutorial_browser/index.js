// 自身のstateを監理しないコンポーネント = 制御されたコンポーネント
class Square extends React.Component {
  render() {
    return (
      // 呼び出し時に格納された変数にもアクセスできる
      // クリックされるまで無名関数は実行されない
      // this.setStateを呼び出すことで、再描画をしている
      // クリックイベントのタイミングでBoardのOnClickで定義した関数が発火する
      <button className="square" onClick={() => this.props.onClick()}>
        {this.props.value}
      </button>
    );
  }
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
    };
  }
  
  // クリック時のイベント定義
  handleClick(i) {
    // 配列をコピー
    const squares = this.state.squares.slice();
    // コピーした配列の中身を変更
    squares[i] = 'X';
    // コピーした配列を代入し、コンポーネントの再描画をする
    this.setState({squares: squares});
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
    const status = 'Next player: X';

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
