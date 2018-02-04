function soNums(e, args) {
    // Função que permite apenas teclas numéricas e  
    // todos os caracteres que estiverem na lista 
    // de argumentos. 
    // Deve ser chamada no evento onKeyPress desta forma 
    //  onKeyPress ="return (soNums(event,'(/){,}.'));" 
    // caso queira apenas permitir caracters 

    if (document.all) { var evt = event.keyCode; } // caso seja IE 
    else { var evt = e.charCode; }    // do contrário deve ser Mozilla 
    var chr = String.fromCharCode(evt);    // pegando a tecla digitada 
    // Se o código for menor que 20 é porque deve ser caracteres de controle 
    // ex.: <ENTER>, <TAB>, <BACKSPACE> portanto devemos permitir 
    // as teclas numéricas vão de 48 a 57 
    if (evt < 20 || (evt > 47 && evt < 58) || (args.indexOf(chr) > -1)) { return true; }
    return false;
} 



function Formata(campo, tammax, teclapres, decimal) {
    decimal = Number(decimal);
    if (!decimal) { decimal = 2; }
    var tecla = teclapres.keyCode;
    vr = Limpar(campo.value, "0123456789");
    tam = vr.length;
    dec = decimal

    if (tam < tammax && tecla != 8) { tam = vr.length + 1; }
    if (tecla == 8) { tam = tam - 1; }
    if (tecla == 8 || tecla >= 48 && tecla <= 57 || tecla >= 96 && tecla <= 105) {
        if (tam <= dec) { campo.value = vr; }
        if ((tam > dec) && (tam <= 7)) { campo.value = vr.substr(0, tam - dec) + "," + vr.substr(tam - dec, tam); }
        if ((tam >= 8) && (tam <= 12)) { campo.value = vr.substr(0, tam - dec) + "," + vr.substr(tam - dec, tam); }
    }
}

function ConverteMoeda(num) {
    x = 0;
    if (num < 0) {
        num = Math.abs(num);
        x = 1;
    }

    if (isNaN(num)) num = "0";
    cents = Math.floor((num * 100 + 0.5) % 100);
    num = Math.floor((num * 100 + 0.5) / 100).toString();

    if (cents < 10) cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + '.'
            + num.substring(num.length - (4 * i + 3));
    ret = num + ',' + cents;
    if (x == 1) ret = ' - ' + ret; return ret;
}

function ChangeParcela(tabela, botao, qt) {
    var v = qt > 1 ? 'visible' : 'hidden';
    botao.style.visibility = v;
   
    if (qt == 1) {
        $('#tableparcelas').empty();
    }
}
function ConverterMoedaDecimais(num) {
    var casas = $("#casas").val() == undefined ? 2 : $("#casas").val(),
        n = num,
        c = casas,
        d = ",",
        t = ".",

        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
}
function DesFormataMoeda(valor) {
    if (valor == undefined) { valor = "0,00"; }
    valor = valor.replace(".", "").replace(".", "");
    valor = valor.replace(",", ".");
    return valor;
}


function Limpar(valor, validos) { // retira caracteres invalidos da string 
    var result = "";
    var aux;
    for (var i = 0; i < valor.length; i++) {
        aux = validos.indexOf(valor.substring(i, i + 1));
        if (aux >= 0) { result += aux; }
    }
    return result;
} 	
