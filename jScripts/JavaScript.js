
//קוד לעמוד של המשחק גרירת ציטוטים והתאמה לאתגרים

//משתנים גלובליים
var b = 0;//משתנה המכיל בתוכו את מספר התשובות שנענו נכון
var totalMatchesNum; //משתנה שיכיל את מספר הזוגות המתאימים

//אחרי שהעמוד נטען
$(document).ready(function () {
    //קבלת מספר הזוגות המתאימים
    totalMatchesNum = parseInt(document.getElementById("matchItemsNumSpan").innerHTML);

    $('#check111').click(function () {//בלחיצה על כפתור בדיקת התשובות

        if (b.toString() == totalMatchesNum) {//אם מספר התשובות שנענו נכון תואם למספר הזוגות התואמים שיש למשחק
         
            $('#message001').text("כל הכבוד!");//משוב חיובי           
            document.getElementById("message001").style.color = "green";

        }
        else {//אם המשתמש לא התאים נכון אז
          // משוב שלילי
            $('#message001').text("לא התאמתם נכון");
            document.getElementById("message001").style.color = "red";
        }
        return false;
    });

});


function allowDrop(ev) {//פונקציה המאפשרת את פעולת השחרור
    ev.preventDefault();
}

function drag(ev) {// פונקציה הנותנת ומעבירה את ה-אי.די של הפריט הנגרר
    var currentId = ev.target.id;
    for (var i = 1; i <= totalMatchesNum; i++) {//לולאה המאכסנת את האיידי הרלוונטי של כל פריט נגרר לדאטה
        if (currentId == "drag" + i.toString()) {
            ev.dataTransfer.setData("drag" + i + "Id", ev.target.id);
        }
    }
}

function drop(ev) {//פונקציה שפועלת ברגע שהמשתמש משחרר את הפריט הנגרר

    ev.preventDefault();
    //משתנים גלובליים בתוך הפונקציה
    var data;
    var draggedItemIdNum;//איבר שיכיל את המספר של האיבר שגוררים אותו
    var dropedOnId = ev.target.id;//מה האידי של מי שגוררים עליו
    var dropedOnItemIdNum = dropedOnId.split("p");//פיצול הסטרינג במספר
    dropedOnItemIdNum = dropedOnItemIdNum[1];//מה האידי של מי שגוררים אותו


    for (var i = 1; i <= totalMatchesNum; i++) {//לולאה שלוקחת את האיידי של הפריט הספציפי שאותו גורר המשתמש בזה הרגע

        if (ev.dataTransfer.getData("drag" + i.toString() + "Id") != 0) {//רק במידה והמשתמש הפיל את המפריט על אחד הפריטים התואמים
            data = ev.dataTransfer.getData("drag" + i.toString() + "Id");// קבלת הדאה איידי הרלוונטי
            draggedItemIdNum = i;//הצבת מספר האי די של הפריט הנגרר בתוך משתנה גלובלי
            console.log(draggedItemIdNum, dropedOnItemIdNum);//בדיקת התאמה
        }
    }
    ev.target.appendChild(document.getElementById(data));// הוספת התוכן הגרור על הנגרר

    if (dropedOnItemIdNum == draggedItemIdNum) {//בדיקה ולהעלאת מספר הפריטים הנכונים
        b++;
    }

}

function backToMapBtn() {// בלחיצה על כפתור חזרה למפה
    location.replace("mapVideo.html")
}

function backToGame() {// בלחיצה על כפתור חזרה למשחק ההתאמות
    location.replace("game.aspx")
}









