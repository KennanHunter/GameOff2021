# Github Actions

Github actions allows us to utilize githubs CI to build, test, and distribute our game.

## activation.yml

We use the activation workflow to request a free license from unity. This is important as
unity licenses are based off of the hardware id.

Note that this only has to be run once when you wish to aquire a license, builds will simply
use the secret that generates from this.

[GameCi documentation on the subject](https://game.ci/docs/github/activation)

## build.yml

We use the build workflow to build the project

[GameCi Documentation](https://game.ci/docs/github/getting-started)
[GameCi Builder Workflow Documentation](https://game.ci/docs/github/builder)
