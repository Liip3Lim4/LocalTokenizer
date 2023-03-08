namespace LocalTokenizer.Constants;

public static class TemplateFileConstants
{
    public static string schemaJson = @"
        {
            'description': 'TemplateFile',
            'type': 'object',
            'properties': {
                'env': {
                    'type': 'array',
                    'items': {
                        'type': 'object',
                        'properties': {
                            'envName': {'type': 'string'},
                            'tokens': {
                                'type': 'array',
                                'items': {
                                    'type': 'object',
                                    'properties': {
                                        'tokenName': {'type': 'string'},
                                        'value': {'type': 'string'}
                                    },
                                    'required': [ 'tokenName', 'value']
                                }
                            }
                        },
                        'required': [ 'envName', 'tokens']
                    }
                }
            },
            'required': [ 'env' ]
        }
    ";
}
