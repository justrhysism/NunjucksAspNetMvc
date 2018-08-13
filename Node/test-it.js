var render = require('./renderNunjucks');
var fs = require('fs');
var path = require('path');

var buttonTemplateAbsolute = "C:/Projects/Playground/NunjucksAspNetMvc/Content/templates/button.njk";

render(function(n, result) {
    console.log('Callback:', result);
}, path.relative('.', buttonTemplateAbsolute),
    {})