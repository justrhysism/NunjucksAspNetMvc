var nunjucks = require('nunjucks');

module.exports = function (callback, template, templatesDirectory, data) {
    nunjucks.configure(templatesDirectory);
    
	var result = nunjucks.render(template, data);
	callback(null, result);
};