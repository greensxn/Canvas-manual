var linkList = new Array('Базовое использование', 'Рисование фигур', 'Стили и цвета', 'Атрибуты', 'События');
var pi = Math.PI;

function setAnimation() {
    var frame = document.querySelectorAll('iframe.preview');
    var info = document.querySelectorAll('h3');
    for (var i = 0; i < info.length; i++) {
        info[i].classList.add('wow');
        info[i].classList.add('fadeInRight');
    }
    for (var i = 0; i < frame.length; i++) {
        frame[i].classList.add('wow');
        frame[i].classList.add('flipInX');
    }
};

function dataLoad() {
    applyEgg();

    setAnimation();
    new WOW({
        mobile: false, // включение/отключение WOW.js на мобильных устройствах
    }).init();

    applyTheme(true);
    if (Cookies.get(darkTheme) == 'undefined')
        Cookies.set('darkTheme', 'false', { expires: 2 });
    if (Cookies.get(egg) == 'undefined')
        Cookies.set('egg', 'false', { expires: 2 });
}

function applyTheme(isPageLoad) {
    var label = document.getElementById('themeLabel');
    var page = document.querySelector('.page');
    var links = document.getElementById("list-guide-1") ?.querySelectorAll('ol > li > a');

    if (!isPageLoad && links != null)
        for (var i = 0; i < links.length; i++)
            links[i].style.transition = '0s';

    if (Cookies.get('darkTheme') == 'true') {
        page.classList.add('dark-theme');
        label.style.backgroundColor = '#EEEEEE';
        label.style.color = 'black';
    } else {
        page.classList.remove('dark-theme');
        label.style.backgroundColor = '#262626';
        label.style.color = 'white';
    }

    if (!isPageLoad && links != null)
        setTimeout(() => {
            for (var i = 0; i < links.length; i++)
                links[i].style.transition = '0.2s padding-left, 0.2s color, 0.2s background-color';
        }, 1000);
}

function applyEgg() {
    if (Cookies.get('egg') == 'true')
        CraigToggle(true, false);
    else
        CraigToggle(false, false);
}

function changeEgg(isOn, isTip) {
    Cookies.set('egg', isOn);
    CraigToggle(isOn, isTip);
}

function changeTheme() {
    var theme = Cookies.get('darkTheme') == 'true' ? 'false' : 'true';
    Cookies.set('darkTheme', theme);
    applyTheme(false);
}

function CanvasLoad() {
    if (document.title.includes('Рисование фигур')) {
        Canvas4();
        Canvas5();
        Canvas6();
    }

    var listCoding = document.querySelectorAll('.Coding');
    for (var i = 1; i <= listCoding.length; i++)
        loadCanvas(i);
}

function Canvas4() {
    var canvas = document.getElementById("c4");
    if (canvas.getContext) {
        var ctx = canvas.getContext("2d");

        ctx.fillStyle = 'red';
        ctx.fillRect(50, 50, 100, 100);
        ctx.fillStyle = 'blue';
        ctx.fillRect(100, 75, 50, 50);

        ctx.strokeStyle = 'green';
        ctx.lineWidth = 5;
        ctx.rect(200, 50, 100, 100);
        ctx.stroke();
        ctx.fillStyle = 'yellow';
        ctx.fill();

        ctx.beginPath();
        ctx.strokeStyle = 'aqua';
        ctx.moveTo(100, 150);
        ctx.lineTo(150, 200);
        ctx.stroke();

        ctx.beginPath();
        ctx.strokeStyle = 'maroon';
        ctx.moveTo(150, 150);
        ctx.lineTo(100, 200);
        ctx.stroke();

        ctx.beginPath();
        ctx.lineWidth = 30;
        ctx.strokeStyle = 'black';
        ctx.moveTo(200, 200);
        ctx.lineTo(350, 250);
        ctx.stroke();


        ctx.beginPath();
        var pi = Math.PI;
        ctx.lineWidth = 2;
        ctx.fillStyle = 'pink';
        ctx.arc(90, 275, 50, 2, pi * 2, false);
        ctx.stroke();
        ctx.fill();

        ctx.beginPath();
        var pi = Math.PI;
        ctx.lineWidth = 10;
        ctx.fillStyle = 'gray';
        ctx.arc(225, 300, 50, 0, pi * 2, true);
        ctx.stroke();
        ctx.fill();
    }
}

var isLineDraw = false;

function Canvas5() {
    var canvas = document.getElementById("c5");
    var Counter = 0;
    if (canvas.getContext) {
        var ctx = canvas.getContext("2d");
        canvas.onclick = function (event) {

            if (Counter == 0) {
                ctx.lineWidth = 5;
                ctx.beginPath();
                ctx.moveTo(event.offsetX, event.offsetY);

                ctx.arc(event.offsetX, event.offsetY, 5, 0, pi * 2, false);
                ctx.fillStyle = 'black';

                ctx.stroke();
                ctx.fill();
            }

            if (Counter == 1) {
                ctx.lineTo(event.offsetX, event.offsetY);

                ctx.arc(event.offsetX, event.offsetY, 5, 0, pi * 2, false);
                ctx.fillStyle = 'black';

                ctx.stroke();

                isLineDraw = true;
            }

            if (Counter == 2) {
                Counter = 0;
                ctx.clearRect(0, 0, 400, 400);
                return;
            }

            Counter++;
        }
    }
}

var isCanvas6End = false;

function Canvas6() {
    var canvas = document.getElementById("c6");

    checkWidth(1200, 'Рисование фигур');

    canvas.onclick = function () {
        z = !z;
    }

    var z = true;
    if (canvas.getContext) {
        var ctx = canvas.getContext("2d");
        canvas.onmousemove = function (event) {
            if (z == false) {
                var x = event.offsetX;
                ctx.beginPath();
                ctx.lineWidth = 5;
                ctx.clearRect(0, 0, 400, 400);
                ctx.arc(event.offsetX, event.offsetY, 50, 0, 2 * pi, false);
                ctx.fillStyle = 'yellow';
                ctx.stroke();
                ctx.fill();
                isCanvas6End = true;
            }
        }
    }
}

function btnMenu() {
    var menu = document.getElementById('menu');
    if (menu.style.display == 'block') {
        menu.style.display = 'none';
    } else {
        menu.style.display = 'block';
    }
}

function listClick(num, element) {
    var arrow = document.getElementById("arrow-" + num);
    var list;

    if (num == 1)
        list = document.getElementById("list-guide-1").querySelectorAll('ol > li');
    else
        list = document.getElementById("menuItem" + num).querySelectorAll('nav > a');


    if (list[0].style.display == "none") {
        arrow.style.transform = "rotate(0deg)";
        if (element != null)
            element.style.borderRadius = '20px 20px 0 0';
        for (let elem of list)
            elem.style.display = "flex";
    } else {
        arrow.style.transform = "rotate(180deg)";
        if (element != null)
            element.style.borderRadius = '28px';
        for (let elem of list)
            elem.style.display = "none";
    }
}

function checkWidth(width, pageTitle) {
    if (document.title.includes(pageTitle)) {
        var parent = document.querySelector(".list-canvas").children;
        if (width >= document.documentElement.clientWidth) {
            parent[2].style.display = 'none';
        } else {
            parent[2].style.display = 'block';
        }
    }

}

var isScrollUp = true;
window.addEventListener('scroll', () => {
    var windowWidth = window.innerWidth;
    var btnUp = document.getElementById('btnUp');
    var statusbar = document.getElementById('statusbar');
    var menuItem = document.getElementById('menuItem1');
    var logo = document.getElementById('logo');
    var textLogo = document.getElementById('textLogo');
    var list = document.querySelector('.menuItem').querySelectorAll('nav > a');

    if (window.scrollY > 85) {
        if (isScrollUp) {
            isScrollUp = false;
            btnUp.style.display = 'block';
            statusbar.style.height = '60px';
            textLogo.style.display = 'none';
            if (windowWidth > 750) // PC
                logo.style.left = '5%';
            menuItem.style.justifyContent = 'center';
            for (var i = 0; i < list.length; i++)
                list[i].querySelectorAll('a > span')[0].textContent = '';
            if (windowWidth > 750 && windowWidth <= 1200) // TAB
                menuItem.style.display = 'flex';
        }
    } else {
        if (!isScrollUp) {
            isScrollUp = true;
            btnUp.style.display = 'none';
            statusbar.style.height = '80px';
            textLogo.style.display = 'block';
            if (windowWidth > 750)
                logo.style.left = '2%';
            menuItem.style.justifyContent = 'space-between';
            for (var i = 0; i < list.length; i++)
                list[i].querySelectorAll('a > span')[0].textContent = linkList[i];

            if (windowWidth > 750 && windowWidth <= 1200) // TAB
                menuItem.style.display = 'none';
        }
    }
});

window.onresize = function (event) {
    checkWidth(1200, 'Рисование фигур');
    var statusbar = document.getElementById('statusbar');
    var windowWidth = window.innerWidth;
    var menu = document.getElementById('menu');
    if (windowWidth > 1200) {
        menu.style.display = 'none';
    } else {
        menu.style.display = 'flex';

    }

    if (windowWidth > 750) { // PHONE
        statusbar.style.display = 'flex';
    }
};


$(document).ready(function () {

    $('.tooltip').hide();
    $('#egg').hide();

    var pageLoading = $('.loading'); //процент прочитанного на сайте
    var h = document.documentElement,
        b = document.body,
        st = 'scrollTop',
        sh = 'scrollHeight';
    window.addEventListener('scroll', () => { // ПОДСКАЗКА КРЕЙГА О ВКЛАДКАХ
        if (window.innerWidth > 750) {
            if (window.scrollY > 85)
                $(pageLoading).css('top', '58px');
            else
                $(pageLoading).css('top', '78px');
        } else
            $(pageLoading).css('top', '0px');

        var percent = (h[st] || b[st]) / ((h[sh] || b[sh]) - h.clientHeight) * 100;
        $(pageLoading).css('width', percent + '%');
    });

    var lastTab = null;
    $('.tabName').on('click', function () {
        if (lastTab != null)
            lastTab.removeClass('active');
        else
            $('.tab1').removeClass('active');
        $(this).addClass('active')
        lastTab = $(this);
    });

    $('a[href^="#"]').click(function () {
        var target = $(this).attr('href');
        $('html, body').animate({ scrollTop: $(target).offset().top - 65 }, 800);
    });

    if (window.innerWidth <= 1200 && window.innerWidth > 750)
        $('.list-guide').css('max-height', ($(window).height() - 160) + 'px'); // размер высоты навбара (если не вмещается текст)
    else if (window.innerWidth <= 750)
        $('.list-guide').css('max-height', '300px');

    if (window.innerWidth >= 1200) {


        $('#themeLabel').on('mouseenter', function () {
            var tooltip = $('.tooltip.tip-theme');
            setTimeout(
                function () {
                    if ($('#themeLabel:hover').length != 0)
                        tooltip.fadeIn(200);
                }, 500);
            $(this).on('mouseleave', function () {
                tooltip.fadeOut(200);
            });
        });

        $('.eggCheck').on('click', function () { // ВКЛЮЧИТЬ / ВЫКЛЮЧИТЬ КРЕЙГА
            var toggle = $('#eggToggle');
            if ($(toggle).html() == 'toggle_on') {
                changeEgg(false, true);
            } else {
                changeEgg(true, true);
            }
        });

        $('#siteThemeBtn').on('click', function () {
            if (Cookies.get('darkTheme') == 'true')
                tooltipText('Ой, это ты натворил? 😯', $('.tooltip.tip-egg'), true, 600, 3300);
            else
                tooltipText('Ай, мои глаза 👀', $('.tooltip.tip-egg'), true, 600, 2700);
        });

        setTimeout( // КАНВАС 5
            function () {
                $('.tooltip.tip-can5').fadeIn(250);
                $('#c5').on('click', function () {
                    $('.tooltip.tip-can5').fadeOut(250);
                });
                setTimeout(
                    function () {
                        $('.tooltip.tip-can5').fadeOut(250);
                    }, 5000);
            }, Cookies.get('egg') == 'true' ? 7400 : 1200);

        var canvasTool = setInterval(function () { // КАНВАС 6
            if (isLineDraw) {
                $('.tooltip.tip-can6').fadeIn(250);
                $('#c6').on('click', function () {
                    $('.tooltip.tip-can6').fadeOut(250);
                });
                setTimeout(
                    function () {
                        $('.tooltip.tip-can6').fadeOut(250);
                    }, 6500);
                clearInterval(canvasTool);
            }
        }, 800);

        var egg = setInterval(function () { // ВКЛЮЧИТЬ КРЕЙГА КОГДА АКТИВИРОВАН КАНВАС
            if (isCanvas6End && isLineDraw) {
                clearInterval(egg);
                if ($('#egg').is(":hidden")) {
                    changeEgg(true, true);

                    setTimeout(
                        function () {
                            tooltipText('Привет, я профессор Крейг, твой помощник по сайту!', $('.tooltip.tip-egg'), true, 800, 3900);
                        }, 1000);

                    setTimeout(
                        function () {
                            tooltipText('Круто ты с этим канвасом справился 😱', $('.tooltip.tip-egg'), true, 0, 3500);
                        }, 5000);

                } else {
                    tooltipText('Молодец! У тебя здорово получается 😊', $('.tooltip.tip-egg'), true, 0, 3600);
                }
            }
        }, 800);

        $('#egg').on('mouseenter', function () { // НАВЕДЕНИЕ НА КРЕЙГА
            var num = Math.floor((Math.random() * 2) + 1);
            var say = Math.floor((Math.random() * 12) + 1);
            if (num == 1) {
                $(this).css('padding-left', '60%');
            } else {
                $(this).css('padding-right', '60%');
            }
            if (say == 5)
                tooltipText(':D', $('.tooltip.tip-egg'), true, 0, 900)

            $(this).on('mouseleave', function () {
                $(this).css('padding-left', '0');
                $(this).css('padding-right', '0');
            });
        });

        $('#siteThemeBtn').on('mouseenter', function () { // ПЕРЕКЛЮЧЕНИЕ ТЕМЫ
            var text = $('.tooltip.tip-theme');
            if (Cookies.get('darkTheme') == 'false') {
                tooltipText(
                    '<h4>Тёмная тема</h4><span>Меняет оформление сайта на тёмное</span>',
                    text,
                    false
                );
            } else {
                tooltipText(
                    '<h4>Светлая тема</h4><span>Меняет оформление сайта на светлое</span>',
                    text,
                    false
                );
            }
        });

        $('#siteThemeBtn').on('click', function () { // ТОЖЕ САМОЕ ТОЛЬКО ЧТО БЫ МЕНЯЛСЯ ТЕКСТ
            var toggle = $('#eggToggle');
            var text = $('.tooltip.tip-theme');
            var isToggleOn = toggle.html() == 'toggle_on';
            if (Cookies.get('darkTheme') == 'false') {
                toggle.css('color', isToggleOn ? '#0060B4' : '#262626');
                tooltipText(
                    '<h4>Тёмная тема</h4><span>Меняет оформление сайта на тёмное</span>',
                    text,
                    false
                );
            }
            else {
                toggle.css('color', isToggleOn ? '#7DBBF1' : '#262626');
                tooltipText(
                    '<h4>Светлая тема</h4><span>Меняет оформление сайта на светлое</span>',
                    text,
                    false
                );
            }
            listCanvas.forEach(element => element.setOption('theme', Cookies.get('darkTheme') == 'true' ? 'icecoder' : 'default'));
        });

        window.addEventListener('scroll', () => { // ПОДСКАЗКА КРЕЙГА О ВКЛАДКАХ
            if (window.scrollY > 85)
                for (var i = 1; i < $('#menuItem1 a').length + 1; i++)
                    $('#menuItem1 a:nth-child(' + i + ')').attr('id', i);
            else
                for (var i = 1; i < $('#menuItem1 a').length + 1; i++)
                    $('#menuItem1 a:nth-child(' + i + ')').removeAttr('id');
        });

        setToolTip(); // расставить подсказки
    }
});

var listCanvas = [];
var listCodeText = [];

function loadCanvas(id) { // КОДИНГ В РЕАЛЬНОМ ВРЕМЕИ
    var delay;
    var parent = document.getElementById('Coding' + id);
    if (parent.classList.length > 1)
        var isMute = parent.classList.contains('noActive') || parent.classList.contains('lock');
    var area = parent.children[0];
    var isCanvas = !parent.hasAttribute('codeRun')
    var buttonReset = parent.children.length > 3 ? parent.children[1].children[1] : false;
    var buttonCopy = parent.children[1].children[0];
    var iframe = parent.children.length > 3 ? parent.children[3] : false;
    var isDarkTheme = Cookies.get('darkTheme') == 'true';
    var codeMode = parent.getAttribute('mode');
    var editor = CodeMirror.fromTextArea(area, {
        mode: codeMode,
        lineNumbers: true,
        htmlMode: true,
        lineWrapping: true,
        readOnly: isMute,
        theme: isDarkTheme ? 'icecoder' : 'default'
    });

    var scroller = editor.getScrollerElement();
    if (!isMute)
        fastTip(scroller, 'Псс, кажется этот код забыли заблокировать, если ты уже авторизовался - попробуй-ка что нибудь в нём поменять, я никому не скажу 😎');

    listCanvas.push(editor);
    listCodeText.push(editor.getValue());

    editor.on('change', function () {
        if (iframe != false) {
            clearTimeout(delay);
            delay = setTimeout(canvas, 500);
        }
    });
    editor.setSize('100%', '100%');

    buttonReset.onclick = function () {
        editor.setValue(listCodeText[id - 1]);
    };
    buttonCopy.onclick = function () {
        navigator.clipboard.writeText(editor.getDoc().getValue());
        editor.setSelections([{ 'anchor': { line: 0, ch: 0 }, 'head': { line: 1000, ch: 0 } }]);
    };

    function canvas() {
        var areaText = editor.getValue().replace("var canvas = document.getElementById('canvas');", "var canvas = document.getElementById('canvas" + id + "');");
        var preview = iframe.contentDocument || iframe.contentWindow.document;
        var isCanvasDark = Cookies.get('darkTheme') == 'true' ? '<style> canvas { background: url("../images/setka3_black.png") } </style>' : '';
        preview.open();
        preview.write(
            isCanvas ?
                isCanvasDark +
                "<canvas height=\"199px\" width=\"299\" id=\"canvas" + id + "\" style=\"margin:-8px;\"></canvas>" +
                "<script>" +
                areaText +
                "</script>" :
                areaText
        );
        preview.close();

        $('#siteThemeBtn').on('click', function () {
            preview.open();
            var isCanvasDark = Cookies.get('darkTheme') == 'true' ? '<style> canvas { background: url("../images/setka3_black.png") } </style>' : '';
            preview.write(
                isCanvas ?
                    isCanvasDark +
                    "<canvas height=\"199px\" width=\"299\" id=\"canvas" + id + "\" style=\"margin:-8px;\"></canvas>" +
                    "<script>" +
                    areaText +
                    "</script>" :
                    areaText
            );
            preview.close();
        });
    }

    if (iframe != false)
        canvas();
}

function CraigToggle(isOn, isTip) {
    var toggle = $('#eggToggle');
    var eggCheck = $('.eggCheck');
    var isDarkTheme = Cookies.get('darkTheme') == 'true';
    if (isOn) {
        $('#egg').show(150);
        toggle.html('toggle_on');
        toggle.css('color', isDarkTheme ? '#7DBBF1' : '#0060B4');

        if (isTip)
            tooltipText('Привет, я профессор Крейг, твой помощник по сайту!', $('.tooltip.tip-egg'), true, 800, 3900);
    } else {
        $('#egg').hide(150);
        toggle.html('toggle_off');
        toggle.css('color', isDarkTheme ? '#262626' : '#262626');

        CraigTipToggle(false);
    }
}

function CraigTipToggle(isOn) {
    if (isOn)
        $('.tooltip.tip-egg').css('display', 'block');
    else
        $('.tooltip.tip-egg').css('display', 'none');
}

var isElse = false;

function tooltipText(text, tool, isCraigName, delay, duration) {
    var say;
    if (isCraigName && $('#egg').is(":visible")) {

        var timer = setTimeout(
            function () {
                if (duration != -1 && !isElse)
                    tool.fadeOut(250);
            }, duration);

        isElse = duration == -1;

        tool.html('<h4>Профессор Крейг</h4><span>' + text + '</span>');
        tool.delay(delay).fadeIn(250);
    } else
        tool.html(text);
}

function changeText(element, text) {
    $(element).html(text);
}

function fastTip(element, text) {
    if (text == ' ') {
        CraigTipToggle(false);
        return;
    }

    $(element).on('mouseenter', function () {
        tooltipText(text, $('.tooltip.tip-egg'), true, 0, -1);
    });
    $(element).on('mouseleave', function () {
        CraigTipToggle(false)
    });
}

function startSaying(text, duration) {
    if (Cookies.get('egg') == 'true')
        setTimeout(
            function () {
                tooltipText(text, $('.tooltip.tip-egg'), true, 0, duration);
            }, 1200);
}


function setToolTip() {

    if (document.title.includes('Базовое использование')) { //НАЧАЛЬНАЯ РЕЧЬ КРЕЙГА
        $('#menuItem1 a:nth-child(1)').addClass('menuActive');
        startSaying('Здесь ты можешь найти пример работы с канвасом и то как начать с ним работать, удачи! 😊', 5500);
    } else if (document.title.includes('Рисование фигур')) {
        $('#menuItem1 a:nth-child(2)').addClass('menuActive');
        startSaying('На мой взгляд самая интересная страничка, здесь вы научитесь рисовать на канвасе различные фигуры, от простой линии, до сложных фигур', 6400);
    } else if (document.title.includes('Стили и цвета')) {
        $('#menuItem1 a:nth-child(3)').addClass('menuActive');
        startSaying('Цвета и стили в канвасе, что может быть лучше?', 3200);
    } else if (document.title.includes('Атрибуты')) {
        $('#menuItem1 a:nth-child(4)').addClass('menuActive');
        startSaying('Как и в любом другом теге, у него есть и свои', 2600);
    } else if (document.title.includes('События')) {
        $('#menuItem1 a:nth-child(5)').addClass('menuActive');
        startSaying('Событие – это сигнал от браузера о том, что что-то произошло. Например когда наводите мышкой на картинку - это событие', 7200);
    }
    else if (document.title.includes('Комментарии')) {
        startSaying('Здесь вы можете оставить свой комментарий, или присоединиться к уже существующему обсуждению', 4600);
    }

    $('#menuItem1 a').on('mouseover', function () {
        if ($(this).is('[id]'))
            tooltipText('<strong>Вкладка:</strong><br>' + linkList[$(this).attr('id') - 1], $('.tooltip.tip-egg'), true, 0, -1);
    });
    $('#menuItem1 a').on('mouseout', function () {
        CraigTipToggle(false);
    });

    fastTip('.btnBox button:nth-child(1)', 'Хочешь скопировать этот код?');
    $('.btnBox button:nth-child(1)').on('click', function () {
        tooltipText('Код скопирован', $('.tooltip.tip-egg'), true, 0, 2000);
    });
    fastTip('.btnBox button:nth-child(2)', 'Эта кнопка вернёт прежний код');
    $('.btnBox button:nth-child(2)').on('click', function () {
        tooltipText('Код восстановлен', $('.tooltip.tip-egg'), true, 0, 2000);
    });

    fastTip('#btnUp', 'Хочешь подняться наверх?');
    fastTip('.sapid', 'Наверное какой-то важжжный элемент 🤔');
    fastTip('.social', 'Своих контактов я не даю, но могу посоветовать соцсети моего помощника 😉');

    $('code').on('mouseenter', function () {
        fastTip('code', 'Это тег ' + $(this).text().replace('<', '&lt;').replace('>', '&gt;') + ', он используются для разграничения начала и конца элементов в разметке');
    });
}

// Комментарии
function ReplyComment(ID, text) {
    console.log(5555555);
    document.getElementById('isReplyInput').value = true;
    document.getElementById('replyIdInput').value = ID;

    var textArea = document.getElementById('commentArea');
    textArea.focus();
    textArea.value = text + ', ';
}

$(document).ready(function () {
    $('#commentForm').keydown(function (e) {
        if (e.ctrlKey && e.keyCode == 13) {
            document.getElementById('commentForm').submit();
        }
    });
});

// Отправка кода
