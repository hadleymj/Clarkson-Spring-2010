function blocks = MakeBlocks(data)
lasttime = data(1,1);
numBlocks = 1;
curBlock(1,:) = data(1,:);
curBlockI = 2;
for i = 2:size(data)
    if round(1000*(lasttime + 1/40)) == round(1000*data(i,1))
        curBlock(curBlockI,:) = data(i,:);
        curBlockI = curBlockI + 1;
        lasttime = data(i,1);
    else
        blocks{numBlocks} = curBlock;
        numBlocks = numBlocks + 1;
        curBlock = [0 0 0 0];
        curBlock(1,:) = data(i,:);
        curBlockI = 2;
        lasttime = data(i,1);
    end
end

blocks{numBlocks} = curBlock;

end