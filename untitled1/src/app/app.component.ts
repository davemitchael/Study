import {Input,Component} from '@angular/core';
import {document} from "@angular/platform-browser/src/facade/browser";
import {isUndefined} from "util";
import {isNull} from "util";
import {isNullOrUndefined} from "util";




@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  level:string;
  UserName:string = 'Dave';
  UserSurename:string = 'Mitchael';
}

@Component({
  selector: 'app-child',
  templateUrl: './app.testblocks.html',
  styleUrls: ['./app.test.blocks.css'],

})
export class ChildComponent {

  data: Question[] = [
    {
      level: 'Новачок', text: 'Переживаєте за успіх в роботі ?', answer: [
      {text: 'сильно', mark: 5},
      {text: 'не дуже', mark: 3},
      {text: 'спокійний', mark: 2}]
    },
    {
      level: 'Новачок', text: 'Прагнете досягти швидко результату ?', answer: [
      {text: 'дуже', mark: 5},
      {text: 'якомога швидше', mark: 3},
      {text: 'поступово', mark: 2}]
    },
    {
      level: 'Новачок', text: 'Чи потрібен чіткий алгоритм для вирішення задач ?', answer: [
      {text: 'так', mark: 5},
      {text: 'в окремих випадках', mark: 3},
      {text: 'не потрібен', mark: 2}]
    },
    {
      level: 'Твердий початківець', text: 'Чи потрібен чіткий алгоритм для вирішення задач ?', answer: [
      {text: 'зрідка', mark: 5},
      {text: 'частково', mark: 3},
      {text: 'ні', mark: 2}]
    },
    {
      level: 'Твердий початківець', text: 'Чи користуєтесь фіксованими правилами  для вирішення задач ?', answer: [
      {text: 'не потрібні', mark: 5},
      {text: 'в окремих випадках', mark: 3},
      {text: 'так', mark: 2}]
    },
    {
      level: 'Твердий початківець', text: 'Чи відчуваєте ви загальний контекст вирішення задачі ?', answer: [
      {text: 'не потрібні', mark: 5},
      {text: 'в окремих випадках', mark: 3},
      {text: 'так', mark: 2}]
    },
    {
      level: 'Компетентний', text: 'Чи можете ви побудувати модель вирішуваної задачі ?', answer: [
      {text: 'так', mark: 5},
      {text: 'не повністю', mark: 3},
      {text: 'в окремих випадках', mark: 2}]
    },
    {
      level: 'Компетентний', text: 'Чи вистачає вам ініціативи при вирішенні задач ?', answer: [
      {text: 'так', mark: 5},
      {text: 'зрідка', mark: 3},
      {text: 'потрібне натхнення', mark: 2}]
    },
    {
      level: 'Компетентний', text: 'Чи можете вирішувати проблеми, з якими ще не стикались ?', answer: [
      {text: 'ні', mark: 5},
      {text: 'в окремех випадках', mark: 3},
      {text: 'так', mark: 2}]
    },
    {
      level: 'Досвідчений', text: 'Чи  необхідний вам весь контекст задачі  ?', answer: [
      {text: 'так', mark: 5},
      {text: 'в окремех випадках', mark: 3},
      {text: 'в загальному', mark: 2}]
    },
    {
      level: 'Досвідчений', text: 'Чи переглядаєте ви свої наміри до вирішення задачі ?', answer: [
      {text: 'так', mark: 5},
      {text: 'зрідка', mark: 3},
      {text: 'коли є потреба', mark: 2}]
    },
    {
      level: 'Досвідчений', text: 'Чи здатні  ви  навчатись у інших ?', answer: [
      {text: 'так', mark: 5},
      {text: 'зрідка', mark: 3},
      {text: 'коли є потреба', mark: 2}]
    },
    {
      level: 'Експерт', text: 'Чи обираєте ви нові методи своєї роботи ?', answer: [
      {text: 'так', mark: 5},
      {text: 'вибірково', mark: 3},
      {text: 'вистачає досвіду', mark: 2}]
    },
    {
      level: 'Експерт', text: 'Чи допомагає власна інтуїція при вирішенні задач ?', answer: [
      {text: 'так', mark: 5},
      {text: 'частково', mark: 3},
      {text: 'при емоційному напруженні', mark: 2}]
    },
    {
      level: 'Експерт', text: 'Чи застовуєте рішення задач за аналогією ?', answer: [
      {text: 'часто', mark: 5},
      {text: 'зрідка', mark: 3},
      {text: 'тільки власний варіант', mark: 2}]
    }
  ];



  score:UserScore[] = [
    {
      level: 'Новачок', score:0
    },
    {
      level: 'Твердий початківець', score:0
    },
    {
      level: 'Компетентний', score:0
    },
    {
      level: 'Досвідчений', score:0
    },
    {
      level: 'Експерт', score:0
    }

  ];


  Dat: Question[] = [];
  evaluation:number[] = [];
  Sum:number=0;
  @Input() levelChild: string;
  @Input() userName:string;
  @Input() userSureName:string;



  condition: boolean = false;
  flag:boolean = false;

  parseData(): Question[] {

if(isNullOrUndefined(this.userName)||isNullOrUndefined(this.userSureName)|| isNullOrUndefined(this.levelChild))
{
  alert("Введіть Ім'я та Прізвище і виберіть рівень");
}
else
  {
  for (let i = 0; i < this.data.length; i++)
  {
    if (this.data[i].level === this.levelChild)
    {
      this.Dat.push(this.data[i]);
    }
    else if(this.levelChild === 'Усі')
    {
      this.Dat.push(this.data[i]);
    }
  }
  (<HTMLInputElement> document.getElementById("qwer")).disabled = true;
  this.condition = true;
  return this.Dat;
  }
  }

  clean(): void {
    (<HTMLInputElement> document.getElementById("qwer")).disabled = false;
    this.condition = false;
    this.flag = false;
    this.Dat.length = 0;
    this.evaluation.length = 0;



  }

  suma():void {
    this.flag = true;
    for (let i = 0; i < this.Dat.length; i++) {
      for (let j = 0; j < 3; j++) {
        if ((<HTMLInputElement>document.getElementsByName('option_question_' + this.Dat[i].level + i))[j].checked) {
          this.evaluation.push(parseInt((<HTMLInputElement>document.getElementsByName('option_question_' + this.Dat[i].level + i))[j].value));

          this.Sum =  this.evaluation.reduce((a:number, b:number) => a + b, 0);
        }
        }
        if (this.evaluation.length === 3){
        for (let k = 0; k < this.score.length; k++) {
          if ((this.score[k].level === this.Dat[i].level)) {
            this.score[k].score = this.Sum;
            this.evaluation.length = 0;
            console.log("this.score[k].score: " + this.score[k].score + "\n" + "this.score[k].level" + this.score[k].level + '\n');
          }
        }
      }
    }
    //console.log("this.Dat[i].level+i: " + this.Dat[i].level+'\n');
    console.log("evaluation: " + this.evaluation + "\n");
    console.log("Sum: " + this.Sum + "\n");
    console.log("this.Dat.length: " + this.Dat.length + "\n");

    this.drawGraphic();
  }

  drawGraphic():void
{
  this.Dat.length = 0;
  for(let i = 0; i < this.score.length; i++)
  {
    //console.log((<HTMLInputElement>document.getElementsByName(i))[0].height);
    (<HTMLInputElement>document.getElementsByName(i))[0].height = ((this.score[i].score/15) * 100);
    (<HTMLInputElement>document.getElementsByName(i))[0].alt = ((this.score[i].score/15) * 100);
  }

}

  reply() {
  for (let i = 0; i < this.Dat.length; i++) {
    for (let j = 0; j < this.Dat.length; j++)
      if ((<HTMLInputElement>document.getElementsByName('option_question_' + i))[j].checked) {
        this.evaluation.push(parseInt((<HTMLInputElement>document.getElementsByName('option_question_' + i))[j].value));
      }
  }



  this.condition = false;
  console.log(this.evaluation);
  this.Sum = this.evaluation.reduce((a:number, b:number) => a + b, 0);
  this.Dat.length = 0;
  this.flag=true;
  console.log(this.Sum);

}
}

class Question
{
  level: string;
  text: string;
  answer:Answer[];
  constructor(Level:string,Text:string,ansver:Answer[]){
    this.level = Level;
    this.text = Text;
    this.answer = ansver;

  }

}
class Answer
{
  text:string;
  mark:number;
  constructor(Text:string,Mark:number)
  {
    this.text = Text;
    this.mark = Mark;
  }

}

class UserScore
{
  level:string;
  score:number;
  constructor(inLevel:string, inScore:number)
  {
    this.level = inLevel;
    this.score = inScore;
  }
}


