var nunjucks = require('nunjucks');

module.exports = function (callback, template, data) {
	// callback(null,  template);
	var result = nunjucks.render(template, data);
	callback(null, result);
};